using Microsoft.AspNetCore.Mvc;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using server_estimation.Models;
using server_estimation.SenderE;
using System.Text;

namespace server_estimation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreateSurveyController : Controller
    {
        private readonly EstimationDbContext _dbcontext;
        public CreateSurveyController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }
        [HttpPost]
        public async Task<IActionResult> CreateSurvey([FromBody] SurveyContract request)
        {
            Survey survey = new Survey(request.title, request.description);
            try
            {
                //внесение изменений в БД
                await _dbcontext.Survey.AddAsync(survey);

                for (int i = 0; request.idQ.Length > i; i++ )
                {
                    Question question = new Question { TitleQuestion = request.titleQuestion[i], Description = request.descriptionQuestion[i], Survey = survey };
                    await _dbcontext.Question.AddAsync(question);


                }

                //сохранение изменений
                await _dbcontext.SaveChangesAsync();
                Console.WriteLine("Опрос был создан успешно");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Произошла ошибка: ", ex.ToString());
            }
            return Ok();
        }
    }
}
