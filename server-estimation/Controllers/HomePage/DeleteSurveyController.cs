using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_estimation.Contracts;
using server_estimation.DataAccess;

namespace server_estimation.Controllers.HomePage
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteSurveyController : Controller
    {
        private readonly EstimationDbContext _dbcontext;
        public DeleteSurveyController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSurvey([FromBody] SurveyId request)
        {
            try
            {
                Console.WriteLine("Начинаем проверку");
                //есть ли опрос с данным id
                var examination = await _dbcontext.Survey.FindAsync(request.Id);
                if (examination != null)
                {
                    Console.WriteLine("Продолжаем проверку");
                    // Удалите пользователя
                    //_dbcontext.Users.Remove(examination);
                    //var authors = _dbcontext.Survey.Include(a => a.Id == request.Id).Include(a => a.Q).ToList();
                    //var authors = await  _dbcontext.Survey.Where(a => a.Id == request.Id).ToListAsync();
                    var authorss = await _dbcontext.Answers.Include(a => a.Questions).Include(o => o.Questions.Survey).Where(s => s.Questions.Survey.Id == request.Id).ToListAsync();

                    // Удаляем все продукты, связанные с заказами
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
                return StatusCode(400);
            }

        }
    }
}

