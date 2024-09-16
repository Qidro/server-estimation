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
        public async Task<IActionResult> RegistrationUser()
        {
            Console.WriteLine("все ок");
            return Ok();
        }
        //тестовые контроллер страницы регистрации
        [HttpPost]
        public async Task<IActionResult> RegistrationUser([FromBody] CreateUser request)
        {
            Console.WriteLine("Я здесь");
            //отправка полученных данных в обьект модели
            var user = new Users(request.Login, request.FirstName, request.LastName, request.Patronymic, request.Email, request.Password);

            //внесение изменений в БД
            await _dbcontext.User.AddAsync(user);
            //сохранение изменений
            await _dbcontext.SaveChangesAsync(); 
            Console.WriteLine("Пользователь зареган");
            return Ok();
        }
    }
}
