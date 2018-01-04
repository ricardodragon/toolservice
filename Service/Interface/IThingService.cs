using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using toolservice.Model;

namespace toolservice.Service.Interface
{
    public interface IThingService
    {
        Task<(Thing, HttpStatusCode)> getThing(int thingId);
    }
}