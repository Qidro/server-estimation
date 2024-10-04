using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace server_estimation.Controllers
{
    [ApiController]
    [Route("[controller]")]

    
    public class CheckJWT : Controller
    {
        [HttpPost]
        public async Task<IActionResult> JWTCheck(string token)
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
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                // Если токен валиден, дальнейшая обработка
                Console.WriteLine("Токен действителен!");
                // Например, можно получить пользовательские claims
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Console.WriteLine($"User ID: {userId}");
            }
            catch (SecurityTokenExpiredException)
            {
                Console.WriteLine("Токен истек");
                return Ok("Токен истек");
            }
            catch (SecurityTokenException)
            {
                Console.WriteLine("Токен недействителен");
                return Ok("Токен недействителен");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка проверки токена: {ex.Message}");
                return Ok("Ошибка проверки токена");
            }
            return Ok("Токен успешно проверен");
        }
    }
}
