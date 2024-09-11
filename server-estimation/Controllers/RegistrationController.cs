using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_estimation.Contracts;

namespace server_estimation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> RegistrationUser([FromBody] CreateUser request)
        {

            return Ok();
        }
    }
}
