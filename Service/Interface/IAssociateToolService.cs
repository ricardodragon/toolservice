using System.Threading.Tasks;
using toolservice.Model;

namespace toolservice.Service.Interface
{
    public interface IAssociateToolService
    {
        Task<(Tool, string)> AssociateTool(int thingId, int toolId);
        Task<(Tool, string)> AssociateTool(int thingId, int toolId, int? position);
        Task<(Tool, string)> DisassociateTool(Tool tool);
        Task<(Tool, string)> AssociateWithoutPosition(int thingId, int toolId);
        Task<(Tool, string)> AssociateWithPosition(int thingId, int toolId, int? position);
    }

}