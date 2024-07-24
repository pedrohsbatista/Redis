using Microsoft.AspNetCore.Mvc;
using Redis.Model.Services;

namespace Redis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CboController : ControllerBase
    {
        private readonly CboService _cboService;

        public CboController(CboService cboService)
        {
            _cboService = cboService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var results = await _cboService.GetAll();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _cboService.GetById(id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
