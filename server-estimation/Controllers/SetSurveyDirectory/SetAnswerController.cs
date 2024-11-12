﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using static server_estimation.Controllers.SetSurveyDirectory.SetSurveyController;
using static server_estimation.Controllers.SetSurveysController;

namespace server_estimation.Controllers.SetSurveyDirectory
{
    [ApiController]
    [Route("[controller]")]
    public class SetAnswerController : Controller
    {
        private readonly EstimationDbContext _dbcontext;
        public SetAnswerController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }

        [HttpPost]
        public async Task<IActionResult> SetAnswers([FromBody] SurveyId request)
        {
            try
            {
                var answers = await _dbcontext.Answers.Include(a => a.Questions).Where(a => a.QuestionId == a.Questions.Id && a.Questions.SurveyId == request.Id).ToListAsync();
                var asnwerList = new List<AnswerList>();

                foreach (var theAnswer in answers)
                {
                    asnwerList.Add(new AnswerList
                    {
                        Id = theAnswer.Id,
                        Question = theAnswer.Question,
                        Comment = theAnswer.Comment,
                        Points = theAnswer.Points,
                        QuestionId = theAnswer.QuestionId

                    });

                }

                return Ok(asnwerList);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка:"+ ex.ToString());
                return Ok();
            }
            
        }

        public class AnswerList
        {
            public int Id { get; set; }

            public string Question { get; set; }

            public string Comment { get; set; }

            public int Points { get; set; }

            public int QuestionId { get; set; }
        }
    }
}
