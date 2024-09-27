using Microsoft.AspNetCore.Mvc;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using server_estimation.Models;
using server_estimation.SenderE;
using System.Security.Cryptography;
using System.Text;

namespace server_estimation.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class RecoveryPasswordController : Controller
    {
        private readonly EstimationDbContext _dbcontext;
        public RecoveryPasswordController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }

        [HttpPost]
        public async Task<IActionResult> RecoveryPassword([FromBody] RecoveryPasswords request)
        {
            //есть ли пользователь с данной почтой
            bool examination = _dbcontext.Users.Any(u => u.Email == request.Email);
            if (examination == true)
            {
                //создаем пользователю новый пароль
                Random random = new Random();

                string password = random.Next(100000000, 999999999).ToString();

                EmailSender emailService = new EmailSender();

                //отправляем на почту пользователю пароль
                await emailService.SendEmailAsync(request.Email, "Confirm your account",
                    $"Ваш новый пароль: {password}");

                //обнолвяем его пароль
                var department = _dbcontext.Users.Where(d => d.Email == request.Email).First();

                using SHA256 hash = SHA256.Create();
                password = Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(password)));
                department.Password = password;
                _dbcontext.SaveChanges();

                
            }
            else {
                return Ok("Ошибка восстановления!");
            }
            return Ok();
        }
    }
 }
