using System.Threading.Tasks;
using toolservice.Model;

namespace toolservice.Service.Interface
{
    public interface IAssociateToolService
    {
        Task<(Tool, string)> AssociateTool(int thingId, int toolId);
        Task<(Tool, string)> DisassociateTool(Tool tool);

    }

}