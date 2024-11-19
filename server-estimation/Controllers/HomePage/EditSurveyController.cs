using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using server_estimation.Models;

namespace server_estimation.Controllers.HomePage
{
    [ApiController]
    [Route("[controller]")]
    public class EditSurveyController : Controller
    {
        private readonly EstimationDbContext _dbcontext;
        public EditSurveyController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }

        //редактирование опроса
        [HttpPut]
        public async Task<IActionResult> EditSurveys([FromBody] EditSurveyContract request)
        {

            try
            {
                Console.WriteLine("Начинаем проверку");
                //есть ли опрос с данным id
                var examination = await _dbcontext.Survey.FindAsync(request.Id);
                if (examination != null)
                {
                    Console.WriteLine("Продолжаем проверку");
                    //_dbcontext.Users.Remove(examination);
                    //var authors = _dbcontext.Survey.Include(a => a.Id == request.Id).Include(a => a.Q).ToList();
                    //var authors = await  _dbcontext.Survey.Where(a => a.Id == request.Id).ToListAsync();
                    var authorss = await _dbcontext.Answers.Include(a => a.Questions).Include(o => o.Questions.Survey).Where(s => s.Questions.Survey.Id == request.Id).ToListAsync();

                    // Удаляем опрос
                    foreach (var questuin in authorss)
                    {
                        _dbcontext.Question.RemoveRange(questuin.Questions);
                        _dbcontext.Survey.RemoveRange(questuin.Questions.Survey);
                    }
                    _dbcontext.Answers.RemoveRange(authorss);
                    // Удаляем все продукты, связанные с заказами
                    //foreach (var answers in authorss)
                    //{
                    //    _dbcontext.Survey.RemoveRange(answers.s);
                    //}
                    //  _dbcontext.Question.RemoveRange(authorss.Questions);

                    //  _dbcontext.Survey.RemoveRange(authorss);
                    // Сохраните изменения в БД
                    Survey survey = new Survey(request.title, request.description);
                    //внесение изменений в БД
                    await _dbcontext.Survey.AddAsync(survey);
                    //поулчаем середину массива
                    //int LenIdQ = request.IdQuestion.Length / 2;
                    
                    //сохраняем изменения 
                    for (int i = 0; request.idQ.Length > i; i++)
                    {
                        Question question = new Question { TitleQuestion = request.titleQuestion[i], Description = request.descriptionQuestion[i], Level = request.level[i], Survey = survey };
                        await _dbcontext.Question.AddAsync(question);


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
                    _dbcontext.SaveChanges();
                    Console.WriteLine("Опрос был удален");
                    return Ok();
                }
                else
                {
                    Console.WriteLine("Такого опроса нет");
                    return StatusCode(404);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла проблема" + e.ToString());
                return StatusCode(418);
            }
        }
    }
}
