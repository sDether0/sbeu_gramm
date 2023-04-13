using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SBEU.Gramm.Api.Controllers
{
    [EnableCors("local")]
    [Route("[controller]")]
    public class PingController : Controller
    {

        [HttpGet("/ping")]
        public async Task<IActionResult> Ping(){
            return Ok("ping pong");
        }
    }
}
