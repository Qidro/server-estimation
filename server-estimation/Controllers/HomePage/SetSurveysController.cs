using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_estimation.DataAccess;
using server_estimation.Models;

namespace server_estimation.Controllers.HomePage
{
    [ApiController]
    [Route("[controller]")]
    public class SetSurveysController : Controller
    {
        private readonly EstimationDbContext _dbcontext;
        public SetSurveysController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }
        [HttpGet]
        //получение списка опросов
        public async Task<IActionResult> SetSurveyList()
        {
            try
            {
                var surveys = await _dbcontext.Survey.ToListAsync();
                var surveyList = new List<SurveyList>();

                //await Task.WhenAll(surveys);
                foreach (var theSurvey in surveys)
                {
                    surveyList.Add(new SurveyList
                    {
                        Id = theSurvey.Id,
                        Title = theSurvey.TitleSurvey,
                        Description = theSurvey.Description

                    });
                }
                return Ok(surveyList);
            }
            catch (Exception ex)
            {
                //если произошла ошибка - заполняем пустой список
                Console.WriteLine("Произошла ошибка: " + ex.ToString);
                var survey = new List<SurveyList>();

                survey.Add(new SurveyList
                {
                    Id = 1,
                    Title = " ",
                    Description = ""
                });
                return Ok(survey);
            }

        }

        public class SurveyList()
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

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
