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
    public class AssociateToolService : IAssociateToolService
    {
        private readonly IToolService _toolService;
        private readonly IToolTypeService _toolTypeService;
        private readonly IStateManagementService _stateManagementService;
        private readonly IThingGroupService _thingGroupService;

        public AssociateToolService(IStateManagementService stateManagementService,
            IToolTypeService toolTypeService,
            IToolService toolService,
            IThingGroupService thingGroupService)
        {
            _toolTypeService = toolTypeService;
            _toolService = toolService;
            _stateManagementService = stateManagementService;
            _thingGroupService = thingGroupService;
        }
        public async Task<(Tool, string)> AssociateTool(int thingId, int toolId)
        {
            var tool = await _toolService.getTool(toolId);
            if (tool == null)
                return (null, "Tool Not Found");
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
            tool = await _toolService.getTool(toolId);
            return (tool, "Tool Set to Use");
        }
    }
}