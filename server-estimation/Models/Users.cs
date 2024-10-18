using Microsoft.AspNetCore.Identity;

namespace server_estimation.Models
{
    public class Users
    {
        public Users(string login, string firstName, string lastName, string patronymic, string email,string tokenEmail, bool confirmedEmail, string role, string password)
        {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Email = email;
            TokenEmail = tokenEmail;
            ConfirmedEmail = confirmedEmail;
            Role = role;
            Password = password;
        }

        public int Id { get; set; }

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string Email { get; set; }

        public string TokenEmail { get; set; }

        public bool ConfirmedEmail { get; set; }

        public string Role { get; set; }

        public string Password { get; set; }
    }
}
