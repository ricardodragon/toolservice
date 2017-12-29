using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using toolservice.Service.Interface;
using toolservice.Model;

namespace toolservice.Controllers
{
    [Route("api/[controller]")]
    public class ToolController : Controller
    {
        private readonly IToolService _toolService;

        public ToolController (IToolService toolService)
        {
            _toolService = toolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery]int startat,[FromQuery]int quantity,
        [FromQuery]string fieldFilter, [FromQuery]string fieldValue,
            [FromQuery]string orderField, [FromQuery]string order)
        {
            try{
            
            var fieldFilterEnum = ToolFieldEnum.Default;
            Enum.TryParse(fieldFilter, true, out fieldFilterEnum);
            var orderFieldEnum = ToolFieldEnum.Default;
            Enum.TryParse(orderField, true, out orderFieldEnum);
            var orderEnumValue = OrderEnum.Ascending;
            Enum.TryParse(order, true, out orderEnumValue);

            if (quantity == 0)
                quantity = 50;
            var (tools,count) = await _toolService.getTools(startat,quantity,fieldFilterEnum,fieldValue,orderFieldEnum,orderEnumValue);
            return Ok(new { values = tools, total = count });
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            try{
                var tool = await _toolService.getTool(id);
                return Ok(tool);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Tool tool)
        {
            try
            {
                if(ModelState.IsValid)
                {
                tool.id = 0;

                    tool = await _toolService.addTool(tool);
                    return Created($"api/Extract/{tool.id}",tool);
                }
                return BadRequest(ModelState);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,[FromBody]Tool tool)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var toolDb = await _toolService.updateTool(id,tool);
                    if (toolDb == null)
                    {
                        return NotFound();
                    }
                    return Ok(toolDb);
                }
                return BadRequest(ModelState);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try{

                if (id>0)
                {

                    var toolDb = await _toolService.deleteTool(id);
                    if (toolDb == null)
                    {
                        return NotFound();
                    }
                    return Ok(toolDb);
                }
                return BadRequest(ModelState);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        
    }
}