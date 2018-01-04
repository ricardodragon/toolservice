using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using toolservice.Service.Interface;
using toolservice.Data;
using toolservice.Model;
using System.Net;

namespace toolservice.Service
{
    public class ToolTypeService : IToolTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IThingGroupService _thingGroupService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Context DB</param>
        public ToolTypeService(ApplicationDbContext context, IThingGroupService thingGroupService)
        {
            _context = context;
            _thingGroupService = thingGroupService;
        }

        /// <summary>
        /// Get in database 
        /// </summary>
        /// <param name="startat">value initial</param>
        /// <param name="quantity">quantity of object return</param>
        /// <returns>list tooltypes</returns>
        public async Task<List<ToolType>> getToolTypes(int startat, int quantity)
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

            if (toolType.thingGroupIds != null && toolType.thingGroupIds.Length != 0)
            {
                var (group, status) = await _thingGroupService.getGroupsList(toolType.thingGroupIds);
                if (status == HttpStatusCode.OK)
                    toolType.thingGroups = group;
            }
            return toolType;
        }

        public async Task<ToolType> updateToolType(int toolTypeId, ToolType toolType)
        {
            var tooltypeDB = await _context.ToolTypes
                     .Where(x => x.id == toolTypeId)
                     .AsNoTracking()
                     .FirstOrDefaultAsync();


            if (toolTypeId != tooltypeDB.id && tooltypeDB == null)
            {
                return null;
            }

            _context.ToolTypes.Update(toolType);
            await _context.SaveChangesAsync();
            return toolType;
        }

        public async Task<ToolType> deleteToolType(int toolTypeId)
        {
            var toolTypeDb = await getToolType(toolTypeId);

            if (toolTypeDb == null)
            {
                return null;
            }

            toolTypeDb.status = "inactive";

            toolTypeDb = await updateToolType(toolTypeId, toolTypeDb);

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