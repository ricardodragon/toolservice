using System.Net.Http;
using System.Threading.Tasks;
using toolservice.Model;

namespace toolservice.Actions
{
    public interface IPostStateChangeAction
    {
        Task action(Tool tool, HttpClient client);
    }
}