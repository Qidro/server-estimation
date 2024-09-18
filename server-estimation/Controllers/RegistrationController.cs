using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using server_estimation.Models;

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

            Console.WriteLine("Я регистрирую пользователя");
            try
            {
                // Проверка на уникальность логина
                bool exists = _dbcontext.Client.Any(u => u.Login == request.Login);
                // Проверка на уникальность логина
                if (exists)
                {
                    Console.WriteLine("Этот логин уже существует.");
                    return Ok("Данный логин занят");
                }
                //отправка полученных данных в обьект модели
                var user = new Clients(request.Login, request.FirstName, request.LastName, request.Patronymic, request.Email, request.Password);

                Console.WriteLine("Наши поля: ",user.Login);
                //внесение изменений в БД
                await _dbcontext.Client.AddAsync(user);
                //сохранение изменений
                await _dbcontext.SaveChangesAsync();
                Console.WriteLine("Пользователь зареган");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: ", e.ToString());
            }
            return Ok();
        }
    }
}
