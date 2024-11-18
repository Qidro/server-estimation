using Microsoft.AspNetCore.Mvc;
using server_estimation.Contracts;

namespace server_estimation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompletingSurveyController : Controller
    {

        [HttpPost]
        public async Task<IActionResult> CompletingSurvey([FromBody] ComplatingQuestions request)
        {

            return View();
        }
    }
}
