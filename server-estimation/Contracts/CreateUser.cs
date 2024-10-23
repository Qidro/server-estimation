namespace server_estimation.Contracts
{
    public record CreateUser(string Login, string FirstName, string LastName, string Patronymic, string Email, string Divisions, string JobTitle, string Password);
}
