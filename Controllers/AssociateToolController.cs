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

        [HttpPut("associate/")]
        [Produces("application/json")]
        public async Task<IActionResult> Associate([FromQuery]int thingId, [FromQuery]int toolId)
        {

            var (tool, result) = await _associateToolService.AssociateTool(thingId, toolId);
            if (tool == null)
                return BadRequest(result);
            return Ok(tool);
        }


        [HttpPut("disassociate/")]
        [Produces("application/json")]
        public async Task<IActionResult> Disassociate([FromBody]Tool tool)
        {
            if (ModelState.IsValid)
            {
                var (returnTool, result) = await _associateToolService.DisassociateTool(tool);
                if (returnTool == null)
                    return BadRequest(result);
                return Ok(tool);
            }
            return BadRequest(ModelState);
        }
    }
}