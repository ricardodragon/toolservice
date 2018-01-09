using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using toolservice.Service.Interface;
using toolservice.Data;
using toolservice.Model;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace toolservice.Service
{
    public class AssociateToolService : IAssociateToolService
    {
        private readonly IToolService _toolService;
        private readonly IToolTypeService _toolTypeService;
        private readonly IStateManagementService _stateManagementService;
        private readonly IThingGroupService _thingGroupService;
        private readonly IConfiguration _configuration;
        private readonly HttpClient client = new HttpClient();

        public AssociateToolService(IStateManagementService stateManagementService,
            IToolTypeService toolTypeService,
            IToolService toolService,
            IThingGroupService thingGroupService,
            IConfiguration configuration)
        {
            _toolTypeService = toolTypeService;
            _toolService = toolService;
            _stateManagementService = stateManagementService;
            _thingGroupService = thingGroupService;
            _configuration = configuration;
        }
        public async Task<(Tool, string)> AssociateTool(int thingId, int toolId)
        {
            var tool = await _toolService.getTool(toolId);
            if (tool == null)
                return (null, "Tool Not Found");
            var availableTools = await _toolService.getToolsAvailable();
            if (availableTools.Select(x => x.id).Contains(toolId))
                return (null, "Tool Not Available");
            var toolType = await _toolTypeService.getToolType(tool.typeId.Value);
            if (toolType == null)
                return (null, "Tool Type Not Found");
            var thingGroups = toolType.thingGroups;
            bool contains = false;
            foreach (var group in thingGroups)
            {
                var (completeGroup, status) = await _thingGroupService.getGroup(group.thingGroupId);
                if (status == HttpStatusCode.OK)
                    contains = completeGroup.things.Select(x => x.thingId).Contains(thingId);
            }
            if (!contains)
                return (null, "This Tool can't  be associated with this thing.");
            await _stateManagementService.setToolToStatusById(toolId, stateEnum.in_use, null);
            await _toolService.setToolToThing(tool, thingId);
            Trigger(tool);
            tool = await _toolService.getTool(toolId);
            return (tool, "Tool Set to Use");
        }

        public async Task<(Tool, string)> DisassociateTool(Tool tool)
        {
            var toolDb = await _toolService.getTool(tool.id);
            if (tool == null)
                return (null, "Tool Not Found");
            if (toolDb.status != stateEnum.in_use.ToString())
                return (null, "Tool Not In Use");
            if (toolDb.currentLife > tool.currentLife)
                return (null, "Current Life can be greater than the previou Life");
            await _stateManagementService.setToolToStatusById(tool.id, stateEnum.available, null);
            await _toolService.setToolToThing(toolDb, null);
            Trigger(tool);
            var newtool = await _toolService.getTool(tool.id);
            return (newtool, "Tool Set to Available");

        }

        private async void Trigger(Tool tool)
        {
            if (_configuration["AssociationPostEndpoint"] != null)
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(JsonConvert.SerializeObject(tool), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(_configuration["AssociationPostEndpoint"], content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Data posted on AssociationPostEndpoint");
                }
            }
        }
    }
}