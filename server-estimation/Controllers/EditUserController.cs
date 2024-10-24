using Microsoft.AspNetCore.Mvc;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using System.Security.Cryptography;
using System.Text;

namespace server_estimation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EditUserController : Controller
    {
        private readonly EstimationDbContext _dbcontext;
        public EditUserController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }

        [HttpPost]
        public async Task<IActionResult> EditUsers([FromBody] EditUsers request)
        {
            try
            {
                Console.WriteLine("Данны пользователя обновляются");
                //есть ли пользователь с данным логином
                var examination = _dbcontext.Users.Where(d => d.Id == request.Id).First();
                if (examination != null)
                {
                    if (request.Password.Length > 0)
                    {
                        Console.WriteLine("Измененте данных с паролем");
                        examination.Login = request.Login;
                        examination.FirstName = request.FirstName;
                        examination.LastName = request.LastName;
                        examination.Patronymic = request.Patronymic;
                        examination.Role = request.Role;
                        using SHA256 hash = SHA256.Create();
                        string password = Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(request.Password)));

                        examination.Password = password;

                        examination.Divisions = request.Divisions;
                        examination.JobTitle = request.JobTitle;
                        

                    }
                    else {
                        Console.WriteLine("Измененте без пароля");
                        examination.Login = request.Login;
                        examination.FirstName = request.FirstName;
                        examination.LastName = request.LastName;
                        examination.Patronymic = request.Patronymic;
                        examination.Role = request.Role;
                        examination.Divisions = request.Divisions;
                        examination.JobTitle = request.JobTitle;
                    }

                    // Сохраните изменения в БД
                    _dbcontext.SaveChanges();
                    Console.WriteLine("Данны пользователя обновлены");
                    return Ok();
                }
                else
                {
                    Console.WriteLine("Такого пользователя нет");
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла в ошибке обновления данных пользователя: ", ex.ToString());
            }
            return Ok();
        }
    }
}
