using Microsoft.AspNetCore.Mvc;

namespace EducacaoOnline.Api.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("API OK");
    }
}
