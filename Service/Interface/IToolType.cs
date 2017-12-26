using System.Threading.Tasks;
using System.Collections.Generic;
using toolservice.Model;

namespace toolservice.Service.Interface
{
    public interface IToolType
    {
         Task<List<ToolType>> getToolTypes(int startat,int quantity);
         Task<ToolType> getToolType(int toolTypeId);
         Task<ToolType> updateToolType(int toolTypeId,ToolType toolType);
         Task<ToolType> deleteToolType(int toolTypeId);
         Task<ToolType> addToolType(ToolType tooltype);

    }
}