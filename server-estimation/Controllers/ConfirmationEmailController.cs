using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using server_estimation.Models;

namespace server_estimation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfirmationEmailController : Controller
    {
        private readonly EstimationDbContext _dbcontext;
        public ConfirmationEmailController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }
        [HttpGet]
        public async Task<IActionResult> ConfirmationToken(string Token)
        {
            try
            {
                // Проверка на уникальность логина
                bool exists = _dbcontext.Users.Any(u => u.TokenEmail == Token);
                // Проверка на уникальность логина
                if (exists)
                {
                    //находим пользователя с данным токеном и меняем состояния поля, что он  подвердил потчу
                    var department = _dbcontext.Users.Where(d => d.TokenEmail == Token).First();
                    department.ConfirmedEmail = true;
                    _dbcontext.SaveChanges();

                    Console.WriteLine("Токен совпал");
                    return Ok("<h1>Все верно<h1>");
                }
                else 
                {
                    Console.WriteLine("Совпадения нет");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка проверки: {ex}");
            }
                return Ok();
        }
    }
}
