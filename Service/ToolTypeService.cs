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
    public class ToolTypeService : IToolType
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Context DB</param>
        public ToolTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ToolType>> getToolTypes(int startat,int quantity)
        {
            var toolTypeId = await _context.ToolTypes
                     .OrderBy(x => x.id)
                     .Skip(startat).Take(quantity)
                     .Select(x => x.id)
                     .ToListAsync();
            List<ToolType> toolTypes = new List<ToolType>();
            foreach (var item in toolTypeId)
            {
                var toolType = await getToolType(item);
                if (toolType != null)
                    toolTypes.Add(toolType);
            }

            return toolTypes;

        }

        public async Task<ToolType> getToolType(int toolTypeId)
        {
            var toolType = await _context.ToolTypes                   
                     .Where(x => x.id == toolTypeId)
                     .FirstOrDefaultAsync();

            
            return toolType;
        }

        public async Task<ToolType> updateToolType(int toolTypeId,ToolType toolType)
        {
            var tooltypeDB = await _context.ToolTypes                    
                     .Where(x => x.id == toolTypeId )
                     .AsNoTracking()
                     .FirstOrDefaultAsync();


            if (toolTypeId != tooltypeDB.id && tooltypeDB == null)
            {
                return null;
            }

            _context.ToolTypes.Update(tooltypeDB);
            await _context.SaveChangesAsync();
            return tooltypeDB;
        }

        public async Task<ToolType> deleteToolType(int toolTypeId)
        {
            var toolTypeDb = await getToolType(toolTypeId);

            if(toolTypeDb == null)
            {
                return null;
            }

            toolTypeDb.status = "desactive";

            toolTypeDb = await updateToolType(toolTypeId,toolTypeDb);

            return toolTypeDb;
        }

        public async Task<ToolType> addToolType(ToolType tooltype)
        {
             _context.ToolTypes.Add(tooltype);
            await _context.SaveChangesAsync();
            return tooltype;
        }
    }
}