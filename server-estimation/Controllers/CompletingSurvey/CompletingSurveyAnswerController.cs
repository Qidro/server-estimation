using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_estimation.Contracts;
using server_estimation.DataAccess;
namespace server_estimation.Controllers.SetSurveyDirectory
{
    [ApiController]
    [Route("[controller]")]
    public class CompletingSurveyAnswerController : Controller
    {
        private readonly EstimationDbContext _dbcontext;
        public CompletingSurveyAnswerController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }

        [HttpPost]
        public async Task<IActionResult> SetAnswers([FromBody] SurveyId request)
        {
            try
            {
                var answers = await _dbcontext.Answers.Include(a => a.Questions)
                    .Where(a => a.QuestionId == a.Questions.Id && a.Questions.SurveyId == request.Id).OrderBy(a => a.QuestionId).ToListAsync();
                var asnwerList = new List<AnswerList>();

                foreach (var theAnswer in answers)
                {
                    asnwerList.Add(new AnswerList
                    {
                        Id = theAnswer.Id,
                        IdQuestion = theAnswer.QuestionId,
                        Question = theAnswer.Question,
                        Comment = theAnswer.Comment,
                        Points = theAnswer.Points

                    });

                }
                return Ok(asnwerList);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка:" + ex.ToString());
                return Ok();
            }

        }

        public class AnswerList
        {
            public int Id { get; set; }
            public int IdQuestion { get; set; }

            public string Question { get; set; }

            public string Comment { get; set; }

            public int Points { get; set; }
        }
    }
}
