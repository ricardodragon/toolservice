using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using toolservice.Data;
using toolservice.Model;
using toolservice.Service.Interface;
using toolservice.Actions;
using System.IO;

namespace toolservice.Service
{
    public class StateManagementService : IStateManagementService
    {
        private readonly StateConfiguration _stateConfiguration;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly HttpClient client;
        private readonly IList<IPostStateChangeAction> _postStateChangeActions;

        private readonly IStateTransitionHistoryService _stateTransitionHistoryService;

        public StateManagementService(ApplicationDbContext context, IConfiguration configuration,
            IList<IPostStateChangeAction> postStateChangeActions,
             IStateTransitionHistoryService stateTransitionHistoryService)
        {
            _postStateChangeActions = postStateChangeActions;
            _context = context;
            _configuration = configuration;
            _stateTransitionHistoryService = stateTransitionHistoryService;
            if (File.Exists("stateconfiguration.json"))
            {
                using (StreamReader r = new StreamReader("stateconfiguration.json"))
                {
                    string json = r.ReadToEnd();
                    _stateConfiguration = JsonConvert.DeserializeObject<StateConfiguration>(json);
                }
            }
            else
                _stateConfiguration = new StateConfiguration();
            if (_configuration["ChangeStatePostEndpoint"] != null)
            {
                client = new HttpClient();

            }
        }


        public async Task<Tool> setToolToStatusById(int toolId, stateEnum newState, Justification justification)
        {
            var tool = await _context.Tools
                                   .Where(x => x.id == toolId)
                                   .FirstOrDefaultAsync();
            if (tool == null)
                return null;
            return await updateTool(tool, newState, justification);
        }

        public async Task<Tool> setTootlToStatusByNumber(string toolSerialNumber, stateEnum newState,
             Justification justification)
        {
            var tool = await _context.Tools
                .Where(x => x.serialNumber == toolSerialNumber)
                .FirstOrDefaultAsync();
            if (tool == null)
                return null;
            return await updateTool(tool, newState, justification);
        }

        public StateConfiguration getPossibleStatusTransition()
        {
            return _stateConfiguration;
        }
        private async Task<Tool> updateTool(Tool tool, stateEnum newState, Justification justification)
        {

            var curState = _stateConfiguration.states
                       .Where(x => x.state == tool.status.ToString()).FirstOrDefault();
            if (curState == null)
                return null;
            var newStateObject = _stateConfiguration.states
                       .Where(x => x.state == newState.ToString()).FirstOrDefault();
            if (newStateObject.needsJustification)
                if (justification == null)
                    return null;
                else if (String.IsNullOrEmpty(justification.text))
                    return null;
            if (curState.possibleNextStates.Contains(newState.ToString()))
            {
                tool.status = newState.ToString();
                _context.Entry(tool).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                await _stateTransitionHistoryService.addToolHistory(tool.id, newStateObject.needsJustification, tool.currentLife,
                    tool, justification, curState.state.ToString(), newState.ToString());
                foreach (var action in _postStateChangeActions)
                {
                    action.action(tool, client);
                }
                return tool;
            }
            return null;
        }


    }
}