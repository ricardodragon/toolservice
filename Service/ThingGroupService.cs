using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using toolservice.Model;
using toolservice.Service.Interface;

namespace toolservice.Service {
    public class ThingGroupService : IThingGroupService {
        private IConfiguration _configuration;
        public ThingGroupService (IConfiguration configuration) {
            _configuration = configuration;
        }
        public async Task<(List<Thing>, HttpStatusCode)> GetAttachedThings (int groupId) {
            HttpClient client = new HttpClient ();

            List<Thing> returnThings = null;
            client.DefaultRequestHeaders.Accept.Clear ();
            client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
            var builder = new UriBuilder (_configuration["thingServiceEndpoint"] + "/api/thinggroups/attachedthings/" + groupId);
            string url = builder.ToString ();
            var result = await client.GetAsync (url);
            switch (result.StatusCode) {
                case HttpStatusCode.OK:
                    returnThings = JsonConvert.DeserializeObject<List<Thing>> (await client.GetStringAsync (url));
                    return (returnThings, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (returnThings, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (returnThings, HttpStatusCode.InternalServerError);
            }
            return (returnThings, HttpStatusCode.NotFound);
        }

        public async Task<(ThingGroup, HttpStatusCode)> getGroup (int groupId) {
            HttpClient client = new HttpClient ();
            ThingGroup returnGroup = null;
            client.DefaultRequestHeaders.Accept.Clear ();
            client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
            var builder = new UriBuilder (_configuration["thingServiceEndpoint"] + "/api/thinggroups/" + groupId);
            string url = builder.ToString ();
            var result = await client.GetAsync (url);
            switch (result.StatusCode) {
                case HttpStatusCode.OK:
                    returnGroup = JsonConvert.DeserializeObject<ThingGroup> (await client.GetStringAsync (url));
                    var (things, status) = await GetAttachedThings (returnGroup.thingGroupId);
                    if (status == HttpStatusCode.OK) {
                        returnGroup.things = things;
                    }
                    return (returnGroup, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (returnGroup, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (returnGroup, HttpStatusCode.InternalServerError);
            }
            return (returnGroup, HttpStatusCode.NotFound);
        }

        public async Task<(List<ThingGroup>, HttpStatusCode)> getGroups (int startat, int quantity,
            string fieldFilter, string fieldValue, string orderField, string order) {
            HttpClient client = new HttpClient ();
            List<ThingGroup> returnGroups = null;
            client.DefaultRequestHeaders.Accept.Clear ();
            client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
            var builder = new UriBuilder (_configuration["thingServiceEndpoint"] + "/api/thinggroups");
            var query = HttpUtility.ParseQueryString (builder.Query);

            if (startat != 0)
                query["startat"] = startat.ToString ();
            if (quantity != 0)
                query["quantity"] = quantity.ToString ();

            if (fieldFilter != null)
                query["fieldFilter"] = fieldFilter.ToString ();
            if (fieldValue != null)
                query["fieldValue"] = fieldValue.ToString ();

            if (orderField != null)
                query["orderField"] = orderField.ToString ();
            if (order != null)
                query["order"] = order.ToString ();

            builder.Query = query.ToString ();
            string url = builder.ToString ();
            var result = await client.GetAsync (url);
            switch (result.StatusCode) {
                case HttpStatusCode.OK:
                    returnGroups = JsonConvert.DeserializeObject<List<ThingGroup>> (await client.GetStringAsync (url));
                    return (returnGroups, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (returnGroups, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (returnGroups, HttpStatusCode.InternalServerError);
            }
            return (returnGroups, HttpStatusCode.NotFound);
        }

        public async Task<(List<ThingGroup>, HttpStatusCode)> getGroupsList (int[] groupIds) {
            HttpClient client = new HttpClient ();
            List<ThingGroup> returnGroups = null;
            client.DefaultRequestHeaders.Accept.Clear ();
            client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
            var builder = new UriBuilder (_configuration["thingServiceEndpoint"] + "/api/thinggroups/list");
            var query = HttpUtility.ParseQueryString (builder.Query);

            builder.Query = query.ToString ();
            string url = builder.ToString () + "?";
            foreach (var item in groupIds) {
                url += $"thingGroupId={item}&";
            }
            var result = client.GetAsync (url).Result;
            switch (result.StatusCode) {
                case HttpStatusCode.OK:
                    string resultstring = await result.Content.ReadAsStringAsync ();
                    returnGroups = JsonConvert.DeserializeObject<List<ThingGroup>> (resultstring);
                    return (returnGroups, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (returnGroups, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (returnGroups, HttpStatusCode.InternalServerError);
            }
            return (returnGroups, HttpStatusCode.NotFound);
        }
    }
}