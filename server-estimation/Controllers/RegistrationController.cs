using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using server_estimation.Models;
using server_estimation.SenderE;
using System.Net;
using System.Security.Cryptography;
using System.Text;

using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

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

                // Генерация кода подтверждения
                string ConfirmationCode = Guid.NewGuid().ToString();
                //отправка полученных данных в обьект модели
                var user = new Users(request.Login, request.FirstName, request.LastName, request.Patronymic, request.Email, ConfirmationCode, false, HashPassword);

                Console.WriteLine("Наши поля: ",user.Login);
                //внесение изменений в БД
                await _dbcontext.Users.AddAsync(user);
                //сохранение изменений
                await _dbcontext.SaveChangesAsync();
                Console.WriteLine("Пользователь зареган");

                //EmailSender b = new EmailSender();
                //await b.SendEmailAsync("pavel.osincev04@mail.ru", "Тема", "Это мое ссобщение");

                // генерация токена для пользователя
                //http://localhost:5281/Registration
                //http://your-app.com/api/auth/confirm?code={ConfirmationCode}

                
                var callbackUrl = $"http://localhost:5281/ConfirmationEmail?Token={ConfirmationCode}";

                EmailSender emailService = new EmailSender();
                await emailService.SendEmailAsync(user.Email, "Confirm your account",
                    $"Подтвердите регистрацию, перейдя по ссылке: <a href=' {callbackUrl} '>link</a>");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: { e.Message}");
            }
            return Ok();
        }
    }
}
