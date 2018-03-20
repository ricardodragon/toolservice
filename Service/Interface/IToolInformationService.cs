using System.Threading.Tasks;
using toolservice.Model;
namespace toolservice.Service.Interface
{
    public interface IToolInformationService
    {
         Task<ToolInformation> addToolInformation(int toolId,ToolInformation toolInformation);
    }
}