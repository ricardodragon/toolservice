using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using toolservice.Model;
using toolservice.Service.Interface;


namespace toolservice.Controllers
{
    [Route("api/tool/[controller]")]
    public class AssociateToolController : Controller
    {
        private readonly IAssociateToolService _associateToolService;
        public AssociateToolController(IAssociateToolService associateToolService)
        {
            _associateToolService = associateToolService;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetGroups([FromQuery]int thingId, [FromQuery]int toolId)
        {

            var (tool, result) = await _associateToolService.AssociateTool(thingId, toolId);
            if (tool == null)
                return BadRequest(result);
            return Ok(tool);
        }


    }
}