using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using System.Security.Cryptography;
using System.Text;


//AuthorizationController
namespace server_estimation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : Controller
    {
        private readonly EstimationDbContext _dbcontext;
        public AuthorizationController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }
        [HttpPost]
        public async Task<IActionResult> AuthorizationUser([FromBody] AuthorizationUsers request)
        {
            try
            {
                var registered = _dbcontext.Users.SingleOrDefault(u => u.Login == request.Login);
                if (registered != null)
                {
                    Console.WriteLine("Пользователь есть");
                    using SHA256 hash = SHA256.Create();
                    string HashPassword = Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(request.Password)));

                    if (registered.Password == HashPassword)
                    {
                        Console.WriteLine("Пароль совпал");
                        if (registered.ConfirmedEmail == true)
                        {
                            Console.WriteLine("Совпадние есть");
                        }
                    }


                    Console.WriteLine("Проверка окончена");
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
