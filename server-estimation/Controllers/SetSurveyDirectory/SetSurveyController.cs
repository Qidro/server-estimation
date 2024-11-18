using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using static server_estimation.Controllers.HomePage.SetSurveysController;

namespace server_estimation.Controllers.SetSurveyDirectory
{
    [ApiController]
    [Route("[controller]")]
    public class SetSurveyController : Controller
    {
        private readonly EstimationDbContext _dbcontext;
        public SetSurveyController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }

        [HttpPost]
        public async Task<IActionResult> SetSurveys([FromBody] SurveyId request)
        {
            try
            {
                //var survey = await _dbcontext.Answers.Include(a => a.Questions).Include(a => a.Questions.Survey).Where(s => s.Questions.Survey.Id == request.Id).ToListAsync();
                var survey = await _dbcontext.Survey.Where(a => a.Id == request.Id).ToListAsync();
                var surveyList = new List<SurveysList>();

                foreach (var theSurvey in survey)
                {
                    surveyList.Add(new SurveysList
                    {
                        //Id = theSurvey.Id,
                        TitleSurvey = theSurvey.TitleSurvey,
                        Description = theSurvey.Description



                    });
                    //surveyList.Add(new SurveysList
                    //{
                    //    Id = theSurvey.Questions.Survey.Id,
                    //    TitleSurvey = theSurvey.Questions.Survey.TitleSurvey,
                    //    Description = theSurvey.Questions.Survey.Description,

                    //    IdQuestion = theSurvey.Questions.Id,
                    //    TitleQuestion = theSurvey.Questions.TitleQuestion,
                    //    DescriptionQuestion = theSurvey.Questions.Description,
                    //    Level = theSurvey.Questions.Level,
                    //    SurveyId = theSurvey.Questions.SurveyId,

                    //    IdAnswer = theSurvey.Id,
                    //    Question = theSurvey.Question,
                    //    Comment = theSurvey.Comment,
                    //    Points = theSurvey.Points,
                    //    QuestionId = theSurvey.QuestionId




                    //});
                }
                Console.WriteLine("Я тут");
                return Ok(surveyList);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ОШИБКА");
                return Ok();
            }
        }
        public class SurveysList
        {
            //сам опрос Survey
            //public int Id { get; set; }
            public string TitleSurvey { get; set; }
            public string Description { get; set; }


        }
        //лист опрос
        //public class SurveysList
        //{
        //    //сам опрос Survey
        //    public int Id { get; set; }

        //    public string TitleSurvey { get; set; }
        //    public string Description { get; set; }

        //    //Вопрос Question

        //    public int IdQuestion { get; set; }

        //    public string TitleQuestion { get; set; }

        //    public string DescriptionQuestion { get; set; }

        //    public int Level { get; set; }
        //    public int SurveyId { get; set; }

        //    //враианта вопроса Answer

        //    public int IdAnswer { get; set; }

        //    public string Question { get; set; }

        //    public string Comment { get; set; }

        //    public int Points { get; set; }

        //    public int QuestionId { get; set; }

        //}
    }
}
