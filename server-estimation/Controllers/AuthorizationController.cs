using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ess;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using server_estimation.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
                //проверка - зареган ли пользователь 
                var registered = _dbcontext.Users.SingleOrDefault(u => u.Login == request.Login);
                if (registered != null)
                {
                    Console.WriteLine("Пользователь есть");
                    //проверк на сооветсвие пароля
                    using SHA256 hash = SHA256.Create();
                    string HashPassword = Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(request.Password)));

                    if (registered.Password == HashPassword)
                    {
                        Console.WriteLine("Пароль совпал");
                        //проверка на подверждение почты
                        if (registered.ConfirmedEmail == true)
                        {   
                            Console.WriteLine("Токен был автивирован");
                            //настройка JWT токена
                            string TokenSession = Guid.NewGuid().ToString();

                            var session = new Sessions(registered.Id, TokenSession);

                            await _dbcontext.Session.AddAsync(session);

                            await _dbcontext.SaveChangesAsync();

                            Claim[] claims = [new("userSession", TokenSession)];

                            var signingCredentials = new SigningCredentials(
                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("goodmorningmyusergoodmorningmyuser")),
                                SecurityAlgorithms.HmacSha256
                                );

                            var token = new JwtSecurityToken(
                                claims: claims,
                                issuer: "USMA",
                                audience:"UserUSMA",
                                signingCredentials: signingCredentials,
                                expires: DateTime.UtcNow.AddMinutes(30)
                                );

                            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

                            return Ok(tokenValue);


                           
                        }
                    }


                    Console.WriteLine("Проверка окончена");
                }
                else
                {
                    Console.WriteLine("Совпадения нет");
                    return Ok("Пользователь не зарегистрирован");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка проверки: {ex}");
            }
            return Ok("Ошибка проверки");
        }

       
    }
}
