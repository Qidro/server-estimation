using Microsoft.AspNetCore.Mvc;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using server_estimation.Models;
using System.Xml.Linq;

namespace server_estimation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SetUsersController : Controller
    {
        private readonly EstimationDbContext _dbcontext;
        public SetUsersController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }
        [HttpPost]
        public async Task<IActionResult> SetUserList()
        {

            try
            {
                // получаем значени из БД
                var allRows = _dbcontext.Users.ToList();

                // создаем коллецию для хранения 
                 var UsersList = new List<СhangeUsers>();

                foreach (var theUsers in allRows)
                {
                    //Console.WriteLine(theGalaxy.Name + "  " + theGalaxy.Email);
                    UsersList.Add(new СhangeUsers
                    {
                        Id = theUsers.Id,
                        Login = theUsers.Login,
                        FullName  = theUsers.LastName +" " + theUsers.FirstName + " " + theUsers.Patronymic,
                        Email = theUsers.Email,
                        ConfirmedEmail = theUsers.ConfirmedEmail
                    });

                }

                Console.WriteLine("Данные о пользователях были получены");
                return Ok(UsersList);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: ", ex.ToString());
                return StatusCode(400);
            }
        }


        public class СhangeUsers
        {
            public int Id { get; set; }
            public string Login { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public bool ConfirmedEmail { get; set; }
        }
    }
}
