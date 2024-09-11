namespace server_estimation.Models
{
    public class Users
    {
        public Users(string firstName, string lastName, string patronymic, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Email = email;
            Password = password;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
