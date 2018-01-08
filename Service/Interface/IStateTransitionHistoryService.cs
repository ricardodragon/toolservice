using System.Collections.Generic;
using System.Threading.Tasks;
using toolservice.Model;

namespace toolservice.Service.Interface
{
    public interface IStateTransitionHistoryService
    {
        Task<IList<StateTransitionHistory>> getToolHistory(int toolid, long from, long to);
        Task addToolHistory(int toolid, bool justificationNeeded, double previoustLife,
             Tool tool, Justification justification, string previousState, string nextState);
    }
}