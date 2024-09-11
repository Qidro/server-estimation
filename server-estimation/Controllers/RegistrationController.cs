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

        //тестовые контроллер страницы регистрации
        [HttpGet]
        public async Task<IActionResult> RegistrationUser([FromBody] CreateUser request)
        {
            //отправка полученных данных в обьект модели
            var user = new Users(request.FirstName, request.LastName, request.Patronymic, request.Email, request.Password);

            //внесение изменений в БД
            await _dbcontext.User.AddAsync(user);
            //сохранение изменений
            await _dbcontext.SaveChangesAsync();

            return Ok();
        }
    }
}
