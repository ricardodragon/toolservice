using System.Collections.Generic;
using System.Threading.Tasks;
using toolservice.Model;

namespace toolservice.Service.Interface {
    public interface IToolService {
        Task<(List<Tool>, int)> getTools (int startat, int quantity, ToolFieldEnum fieldFilter, string fieldValue, ToolFieldEnum orderField, OrderEnum order);
        Task<List<Tool>> getToolsInUSe ();
        Task<List<Tool>> getToolsOnThing (int thingId);
        Task<List<Tool>> getToolsAvailable ();
        Task<Tool> setToolToThing (Tool tool, int? thingId);
        Task<Tool> setToolToPosition (Tool tool, int? position);
        Task<Tool> getTool (int toolId);
        Task<Tool> updateTool (int toolId, Tool tool);
        Task<Tool> deleteTool (int toolId);
        Task<Tool> addTool (Tool tool);
    }

    public enum ToolFieldEnum {
        Default,
        Name,
        Description,
        SerialNumber,
        Code,
        TypeName,
        Status

    }
}