using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using server_estimation.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace server_estimation.Controllers
{
    [ApiController]
    [Route("[controller]")]

    
    public class CheckJWT : Controller
    {
        [HttpPost]
        public async Task<IActionResult> JWTCheck([FromBody] Token request)
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
                    ValidIssuer = "USMA",// можно включить если вам нужно проверять Issuer
                    ValidateAudience = true, // можно включить если вам нужно проверять Audience
                    ValidAudience = "UserUSMA",
                    ClockSkew = TimeSpan.Zero // убираем задержку проверки времени
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
                    Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");

                    if (claim.Type == "user")
                    {
                        login = claim.Value;
                    }
                }
                Console.WriteLine("Логин пользователя:"+ login);
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
