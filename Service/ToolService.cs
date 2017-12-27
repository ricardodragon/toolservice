using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using toolservice.Service.Interface;
using toolservice.Data;
using toolservice.Model;

namespace toolservice.Service
{
    public class ToolService : IToolService
    {
        private readonly ApplicationDbContext _context;
        private readonly IToolTypeService _toolTypeService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Context DB</param>
        public ToolService(ApplicationDbContext context,IToolTypeService toolTypeService)
        {
            _context = context;
            _toolTypeService = toolTypeService;
        }

        public async Task<List<Tool>> getTools(int startat,int quantity)
        {
            var toolId = await _context.Tools
                     .OrderBy(x => x.id)
                     .Skip(startat).Take(quantity)
                     .Select(x => x.id)
                     .ToListAsync();
            List<Tool> tools = new List<Tool>();
            foreach (var item in toolId)
            {
                var tool = await getTool(item);
                if (tool != null)
                    tools.Add(tool);
            }

            return tools;

        }

        public async Task<Tool> getTool(int toolId)
        {
            var tool = await _context.Tools                   
                     .Where(x => x.id == toolId)
                     .FirstOrDefaultAsync();

            var toolType = await _toolTypeService.getToolType(tool.typeId.Value);
            tool.typeName = toolType.name;

            
            return tool;
        }

        public async Task<Tool> updateTool(int toolId,Tool tool)
        {
            var toolDB = await _context.Tools                    
                     .Where(x => x.id == toolId )
                     .AsNoTracking()
                     .FirstOrDefaultAsync();


            if (toolId != toolDB.id && toolDB == null)
            {
                return null;
            }

            _context.Tools.Update(tool);
            await _context.SaveChangesAsync();
            return tool;
        }

        public async Task<Tool> deleteTool(int toolId)
        {
            var toolDb = await getTool(toolId);

            if(toolDb == null)
            {
                return null;
            }

            toolDb.status = "inactive";

            toolDb = await updateTool(toolId,toolDb);

            return toolDb;
        }

        public async Task<Tool> addTool(Tool tool)
        {
             _context.Tools.Add(tool);
            await _context.SaveChangesAsync();
            return tool;
        }


        
    }
}