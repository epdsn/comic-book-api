using Microsoft.AspNetCore.Mvc;

namespace ComicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { message = "Comic Book API is alive" });
        }
    }
}