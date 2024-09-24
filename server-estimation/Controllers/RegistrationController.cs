using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using server_estimation.Migrations;
using server_estimation.Models;
using System.Security.Cryptography;
using System.Text;

namespace server_estimation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : Controller
    {
        private readonly EstimationDbContext _dbcontext;


        public RegistrationController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> RegistrationUserr()
        {
            Console.WriteLine("все ок");
            return Ok();
        }
        //public async Task<IActionResult> RegistrationUser([FromBody] CreateUser request)
        //тестовые контроллер страницы регистрации
        [HttpPost]
        public async Task<IActionResult> RegistrationUser([FromBody] CreateUser request)
        {
            using SHA256 hash = SHA256.Create();
            String HashPassword = Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(request.Password)));
            Console.WriteLine("Я регистрирую пользователя");
            try
            {
                // Проверка на уникальность логина
                bool exists = _dbcontext.Users.Any(u => u.Login == request.Login);
                // Проверка на уникальность логина
                if (exists)
                {
                    Console.WriteLine("Этот логин уже существует.");
                    return Ok("Данный логин занят");
                }
                //отправка полученных данных в обьект модели
                var user = new Users(request.Login, request.FirstName, request.LastName, request.Patronymic, request.Email, false, HashPassword);

                Console.WriteLine("Наши поля: ",user.Login);
                //внесение изменений в БД
                await _dbcontext.Users.AddAsync(user);
                //сохранение изменений
                await _dbcontext.SaveChangesAsync();
                Console.WriteLine("Пользователь зареган");
            
                //попытка подвержедния почты
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: ", e.ToString());
            }
            return Ok();
        }
    }
}
