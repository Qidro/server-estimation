using Microsoft.AspNetCore.Mvc;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using server_estimation.Models;
using server_estimation.SenderE;
using System.Text;

namespace server_estimation.Controllers.HomePage
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
                //поулчаем середину массива
                int LenIdQ = request.IdQuestion.Length / 2;
                for (int i = 0; request.idQ.Length > i; i++)
                {
                    Question question = new Question { TitleQuestion = request.titleQuestion[i], Description = request.descriptionQuestion[i], Level = request.level[i], Survey = survey };
                    await _dbcontext.Question.AddAsync(question);
                    //int j = 0;
                    //if (request.idQ[i] == 0)
                    //{ 

                    //}
                    //else if (request.idQ[i] > request.IdQuestion[LenIdQ])
                    //{
                    //    j = LenIdQ;
                    //}
                    //else if (request.idQ[i] < request.IdQuestion[LenIdQ])
                    //{
                    //    j = request.IdQuestion.Length - LenIdQ;
                    //}
                    //else
                    //{
                    //    while (request.idQ[i] == request.IdQuestion[LenIdQ])
                    //    {
                    //        LenIdQ = LenIdQ - 1;
                    //    }
                    //    j = LenIdQ;
                    //}

                    //for (; request.IdQuestion.Length > j; j++ )
                    //{
                    //    if (request.idQ[i] == request.IdQuestion[j])
                    //    {
                    //        Answers answers = new Answers { Question = request.question[j], Comment = request.comment[j], Points = request.points[j], Questions = question };
                    //        Console.WriteLine("Сохрагя именения ответов");
                    //        await _dbcontext.Answers.AddAsync(answers);
                    //    }
                    //    //else {
                    //    //    break;
                    //    //}
                    //}

                    for (int j = 0; request.IdQuestion.Length > j; j++)
                    {
                        if (request.idQ[i] == request.IdQuestion[j])
                        {
                            Answers answers = new Answers { Question = request.question[j], Comment = request.comment[j], Points = request.points[j], Questions = question };
                            Console.WriteLine("Сохрагя именения ответов");
                            await _dbcontext.Answers.AddAsync(answers);
                        }
                        //else {
                        //    break;
                        //}
                    }

                }

                //сохранение изменений
                await _dbcontext.SaveChangesAsync();
                Console.WriteLine("Опрос был создан успешно");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: ", ex.ToString());
            }
            return Ok();
        }
    }
}
