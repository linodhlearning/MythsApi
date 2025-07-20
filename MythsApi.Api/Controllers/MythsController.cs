using Microsoft.AspNetCore.Mvc;
using MythsApi.Application.Model;
using MythsApi.Application.Interfaces;

namespace MythsApi.Api.Controllers
{
    [ApiController]
    [Route("api/myths")]
    public class MythsController : ControllerBase
    {
        private readonly IMythService _service;

        public MythsController(IMythService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MythCreateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdMyth = await _service.CreateAsync(model);
            return CreatedAtAction(nameof(Get), new { id = createdMyth.Id }, createdMyth);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MythUpdateModel model)
        {
            if (id != model.Id) return BadRequest();
            await _service.UpdateAsync(id, model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}

