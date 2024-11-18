using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using server_estimation.Models;
using System.IdentityModel.Tokens.Jwt;

namespace server_estimation.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CompletingTheSurveyController : Controller
    {

        private readonly EstimationDbContext _dbcontext;
        public CompletingTheSurveyController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }

        [HttpPost]
        public async Task<IActionResult> CompletSurvey([FromBody] ComplatingQuestions request)
        {
            try
            {


                // Создание ключа безопасности
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("goodmorningmyusergoodmorningmyuser"));

                var tokenHandler = new JwtSecurityTokenHandler();
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

                    if (claim.Type == "user")
                    {
                        login = claim.Value;
                    }
                }

                var user = _dbcontext.Users.FirstOrDefault(a => a.Login == login);

                var surveyArray = await _dbcontext.Answers.Include(a => a.Questions).Include(a => a.Questions.Survey).Where(a => a.Questions.Survey.Id == request.IdSurvey).ToListAsync();
                for (int i = 0; request.IdQiestion.Length > i; i++)
                {
                    var answerList = _dbcontext.Answers.FirstOrDefault(a => a.Id == request.IdAnswer[i]);
                    SurveyResults surveyResults = new SurveyResults { Users = user, Questions = answerList.Questions, Answers = answerList, Points = answerList.Points };
                    await _dbcontext.SurveyResults.AddAsync(surveyResults);
                }
                await _dbcontext.SaveChangesAsync();
            }

            catch(Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
                }
            return Ok();
        }
    }
}
