using Microsoft.AspNetCore.Mvc;
using server_estimation.Contracts;
using server_estimation.DataAccess;
using System.Text;

namespace server_estimation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SetUserRequstController : Controller
    {
        private readonly EstimationDbContext _dbcontext;
        public SetUserRequstController(EstimationDbContext dbContext)
        {
            _dbcontext = dbContext;

        }

        [HttpPost]
        public async Task<IActionResult> SetUsersRequst([FromBody] SetUserRequest request)
        {
            // создаем коллецию для хранения 
            var UsersList = new List<СhangeUsers>();
            var UsersListError = new List<СhangeUsersNull>();
            try
            {
                switch (request.titleRequst)
                {
                    case "FullName":
                        {
                            string[] FullName = request.search.Split(" ");
                            if (FullName.Length < 2 || FullName.Length > 3)
                            {
                                //парсим даные в коллекцию
                                UsersListError.Add(new СhangeUsersNull
                                {
                                    Id = " ",
                                    Login = " ",
                                    FullName = " ",
                                    Email = " ",
                                    ConfirmedEmail = " ",
                                    Divisions = " ",
                                    JobTitle = " ",
                                    Role = " "
                                });
                                return Ok(UsersList);
                            }
                            else
                            {


                                if (FullName.Length == 2)
                                {
                                    //запрос к бд по Имени - Отчеству или Имении - Фамилии или Фамилии - отчеству
                                    var examination = _dbcontext.Users
                                    .Where(u => (u.FirstName == FullName[0] && u.Patronymic == FullName[1]) ||
                                                 (u.FirstName == FullName[0] && u.LastName == FullName[1]) ||
                                                 (u.LastName == FullName[0] && u.FirstName == FullName[1]) ||
                                                 (u.LastName == FullName[0] && u.Patronymic == FullName[1]))
                                    .FirstOrDefault();
                                    if (examination != null)
                                    {

                                        //парсим даные в коллекцию
                                        UsersList.Add(new СhangeUsers
                                        {
                                            Id = examination.Id,
                                            Login = examination.Login,
                                            FullName = examination.LastName + " " + examination.FirstName + " " + examination.Patronymic,
                                            Email = examination.Email,
                                            ConfirmedEmail = examination.ConfirmedEmail,
                                            Divisions = examination.Divisions,
                                            JobTitle = examination.JobTitle,
                                            Role = examination.Role
                                        });


                                        return Ok(UsersList);
                                    }
                                    else
                                    {
                                        //парсим даные в коллекцию
                                        UsersListError.Add(new СhangeUsersNull
                                        {
                                            Id = " ",
                                            Login = " ",
                                            FullName = " ",
                                            Email = " ",
                                            ConfirmedEmail = " ",
                                            Divisions = " ",
                                            JobTitle = " ",
                                            Role = " "
                                        });
                                        return Ok(UsersList);
                                    }
                                }
                                else
                                {
                                    //запрос к бд по фИО
                                    var examination = _dbcontext.Users.Where(u => u.FirstName == FullName[1] && u.LastName == FullName[0] && u.Patronymic == FullName[2]
                                    ).FirstOrDefault();
                                    if (examination != null)
                                    {
                                        //парсим даные в коллекцию
                                        UsersList.Add(new СhangeUsers
                                        {
                                            Id = examination.Id,
                                            Login = examination.Login,
                                            FullName = examination.LastName + " " + examination.FirstName + " " + examination.Patronymic,
                                            Email = examination.Email,
                                            ConfirmedEmail = examination.ConfirmedEmail,
                                            Divisions = examination.Divisions,
                                            JobTitle = examination.JobTitle,
                                            Role = examination.Role
                                        });


                                        return Ok(UsersList);
                                    }
                                    else
                                    {
                                        //парсим даные в коллекцию
                                        UsersListError.Add(new СhangeUsersNull
                                        {
                                            Id = " ",
                                            Login = " ",
                                            FullName = " ",
                                            Email = " ",
                                            ConfirmedEmail = " ",
                                            Divisions = " ",
                                            JobTitle = " ",
                                            Role = " "
                                        });
                                        return Ok(UsersList);
                                    }
                                }

                            }

                            break;
                        }

                    case "Email":
                        {
                            //запрос к бд по почте
                            var examination = _dbcontext.Users.Where(u => u.Email == request.search
                            ).FirstOrDefault();
                            if (examination != null)
                            {
                                //парсим даные в коллекцию
                                UsersList.Add(new СhangeUsers
                                {
                                    Id = examination.Id,
                                    Login = examination.Login,
                                    FullName = examination.LastName + " " + examination.FirstName + " " + examination.Patronymic,
                                    Email = examination.Email,
                                    ConfirmedEmail = examination.ConfirmedEmail,
                                    Divisions = examination.Divisions,
                                    JobTitle = examination.JobTitle,
                                    Role = examination.Role
                                });
                                return Ok(UsersList);
                            }
                            else
                            {
                                //парсим даные в коллекцию
                                UsersListError.Add(new СhangeUsersNull
                                {
                                    Id = " ",
                                    Login = " ",
                                    FullName = " ",
                                    Email = " ",
                                    ConfirmedEmail = " ",
                                    Divisions = " ",
                                    JobTitle = " ",
                                    Role = " "
                                });
                                return Ok(UsersList);
                            }
                            break;
                        }
                    case "Login":
                        {
                            //запрос к бд по лоигну
                            var examination = _dbcontext.Users.Where(u => u.Login == request.search
                            ).FirstOrDefault();
                            if (examination != null)
                            {
                                //парсим даные в коллекцию
                                UsersList.Add(new СhangeUsers
                                {
                                    Id = examination.Id,
                                    Login = examination.Login,
                                    FullName = examination.LastName + " " + examination.FirstName + " " + examination.Patronymic,
                                    Email = examination.Email,
                                    ConfirmedEmail = examination.ConfirmedEmail,
                                    Divisions = examination.Divisions,
                                    JobTitle = examination.JobTitle,
                                    Role = examination.Role
                                });
                                return Ok(UsersList);
                            }
                            else
                            {
                                //парсим даные в коллекцию
                                UsersListError.Add(new СhangeUsersNull
                                {
                                    Id = " ",
                                    Login = " ",
                                    FullName = " ",
                                    Email = " ",
                                    ConfirmedEmail = " ",
                                    Divisions = " ",
                                    JobTitle = " ",
                                    Role = " "
                                });
                                return Ok(UsersList);
                            }
                            break;
                        }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: ", ex.ToString());

                return BadRequest(400);
            }
            return Ok();
        }

        public class СhangeUsers
        {
            public int Id { get; set; }
            public string Login { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public bool ConfirmedEmail { get; set; }
            public string Divisions { get; set; }
            public string JobTitle { get; set; }
            public string Role { get; set; }
        }

        public class СhangeUsersNull
        {
            public string Id { get; set; }
            public string Login { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string ConfirmedEmail { get; set; }
            public string Divisions { get; set; }
            public string JobTitle { get; set; }
            public string Role { get; set; }
        }

    }
}
