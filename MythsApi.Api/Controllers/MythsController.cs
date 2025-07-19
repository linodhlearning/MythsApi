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
        public async Task<IActionResult> Create([FromBody] MythModel myth) => Ok(await _service.CreateAsync(myth));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MythModel myth)
        {
            await _service.UpdateAsync(id, myth);
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

