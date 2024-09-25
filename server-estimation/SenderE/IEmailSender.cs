namespace server_estimation.SenderE
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

}
