using Microsoft.AspNetCore.Mvc;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using server_estimation.Models;

namespace server_estimation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteUserController : Controller
    {
        private readonly EstimationDbContext _dbcontext;
        public DeleteUserController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] UserId request)
        {
            try
            {
                //есть ли пользователь с данным id
                var examination = _dbcontext.Users.Find(request.Id);
                if (examination != null)
                {
                    // Удалите пользователя
                    _dbcontext.Users.Remove(examination);

                    // Сохраните изменения в БД
                    _dbcontext.SaveChanges();
                    Console.WriteLine("Пользователь был удален");
                    return Ok();
                }
                else
                {
                    Console.WriteLine("Такого пользователя нет");
                    return StatusCode(400);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Произошла проблема" + e.ToString());
                return StatusCode(400);
            }
            
        }
    }
}
