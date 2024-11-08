using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using System.IdentityModel.Tokens.Jwt;

namespace server_estimation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckAdminRoleController : Controller
    {
        private readonly EstimationDbContext _dbcontext;
        public CheckAdminRoleController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }

        [HttpPost]
        public async Task<IActionResult> CheckRole([FromBody] Token request)
        {
            // Создание ключа безопасности
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("goodmorningmyusergoodmorningmyuser"));

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                // Настройка параметров валидации токена
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = "USMA",
                    ValidateAudience = true,
                    ValidAudience = "UserUSMA",
                    ClockSkew = TimeSpan.Zero
                };

                // Валидация токена
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(request.token, validationParameters, out validatedToken);

                // Если токен валиден, дальнейшая обработка
                Console.WriteLine("Токен действителен!");
                // Например, можно получить пользовательские claims
                var userId = principal.Claims;
                //тестовая перменная для получения логина
                string login = null;
                // Выводим значения claims
                foreach (var claim in userId)
                {
                    if (claim.Type == "user")
                    {
                        login = claim.Value;
                    }
                }
                Console.WriteLine("Логин пользователя:" + login);

                var examination = _dbcontext.Users.Where(d => d.Login == login).First();
                if (examination != null)
                {
                    if (examination.Role == "Администратор")
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return StatusCode(403);
                    }
                }
                else
                {
                    return StatusCode(403);
                }

            }
            catch (SecurityTokenExpiredException)
            {
                Console.WriteLine("Токен истек");
                return StatusCode(401);
            }
            catch (SecurityTokenException)
            {
                Console.WriteLine("Токен недействителен");
                return StatusCode(401);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка проверки токена: {ex.Message}");
                return StatusCode(400);
            }
            return Ok(true);
        }
    }
}
