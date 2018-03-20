using System;
using System.Threading.Tasks;
using toolservice.Model;
using toolservice.Service.Interface;
using toolservice.Data;

namespace toolservice.Service
{
    public class ToolInformationService : IToolInformationService
    {
        private readonly IToolService _toolService;
        private readonly ApplicationDbContext _context;
        public ToolInformationService(IToolService toolService,ApplicationDbContext context)
        {
            _toolService = toolService;
            _context = context;
        }

        public async Task<ToolInformation> addToolInformation(int toolId,ToolInformation toolInformation)
        {
            if(toolInformation.datetime<1)
            {
                toolInformation.datetime = DateTime.Now.Ticks;
            }

            var tool = await _toolService.getTool(toolId);

            if(tool == null)
                return null;

            tool.informations.Add(toolInformation);

            await _context.SaveChangesAsync();        

            return toolInformation;
        }
        
    }
}