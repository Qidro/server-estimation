using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace server_estimation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> RegistrationUser()
        {
            return Ok();
        }
    }
}
