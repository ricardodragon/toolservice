using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using toolservice.Model;

namespace toolservice.Actions
{
    public class TriggerAction : IPostStateChangeAction
    {
        private readonly IConfiguration _configuration;

        public TriggerAction(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async void action(Tool tool, HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(JsonConvert.SerializeObject(tool), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(_configuration["ChangeStatePostEndpoint"], content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data posted");
            }
        }
    }
}