using Microsoft.AspNetCore.Mvc;

namespace ComicBookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthzController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { message = "Comic Book API is alive and well!" });
        }
    }
}