using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using server_estimation.Models;
using static server_estimation.Controllers.SetSurveyDirectory.SetAnswerController;
using static server_estimation.Controllers.SetSurveyDirectory.SetQuestionController;

namespace server_estimation.Controllers.SetSurveyDirectory
{
    [ApiController]
    [Route("[controller]")]
    public class CompletingSurveyQuestionController : Controller
    {
        private readonly EstimationDbContext _dbcontext;
        public CompletingSurveyQuestionController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }
        [HttpPost]
        //получение списка вопросов
        public async Task<IActionResult> SetSurveyList([FromBody] SurveyId request)
        {
            try
            {
                var questions = await _dbcontext.Question.Where(a => a.SurveyId == request.Id).OrderBy(a => a.Level).ToListAsync();
                var questionList = new List<QuestionList>();

                //await Task.WhenAll(surveys);
                foreach (var theQuestion in questions)
                {
                    questionList.Add(new QuestionList
                    {
                        Id = theQuestion.Id,
                        TitleQuestion = theQuestion.TitleQuestion,
                        Description = theQuestion.Description,
                        Level = theQuestion.Level

                    });
                }
                return Ok(questionList);
            }
            catch (Exception ex)
            {
                //если произошла ошибка - заполняем пустой список
                Console.WriteLine("Произошла ошибка: " + ex.Message);

                return Ok(false);
            }

        }

        public class QuestionList
        {
            public int Id { get; set; }

            public string TitleQuestion { get; set; }

            public string Description { get; set; }

            public int Level { get; set; }
        }

        //public async Task<IActionResult> SetSurveysList()
        //{
        //    try
        //    {
        //        //получение данных из БД
        //        var surveys = _dbcontext.Survey.ToList();
        //        var survey = new List<Survey>();
        //        foreach (var theSurvey in surveys)
        //        {
        //            survey.Add(new Survey
        //            {
        //                Id = theSurvey.Id,
        //                Title = theSurvey.TitleSurvey,
        //                Description = theSurvey.Description
        //            });
        //        }
        //        return Ok(survey);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Произошла ошибка: " + ex.ToString);
        //        var survey = new List<Survey>();

        //        survey.Add(new Survey
        //        {
        //            Id = 1,
        //            Title = " ",
        //            Description = ""
        //        });
        //        return Ok(survey);
        //    }

        //}

        //public class Survey
        //{ 
        //    public int Id { get; set; }

        //    public string Title { get; set; }

        //    public string Description { get; set; }
        //}


    }
}
