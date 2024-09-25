using System.Net;
using System.Net.NetworkInformation;

using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;


namespace server_estimation.SenderE
{

    public class EmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string mess)
        {
            // Создаем новое сообщение
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Павел", "pavel.osincev04@mail.ru"));
            message.To.Add(new MailboxAddress("Пользователь", email));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = mess
            };

            // Отправляем сообщение
            using (var client = new SmtpClient())
            {
                try
                {
                    // Подключаемся к SMTP-серверу
                    await client.ConnectAsync("smtp.mail.ru", 587, MailKit.Security.SecureSocketOptions.StartTls);

                    // Вход в почтовый ящик
                    await client.AuthenticateAsync("pavel.osincev04@mail.ru", "6Qjq68WvTh8pedsywQr1");

                    // Отправляем сообщение
                    await client.SendAsync(message);
                    Console.WriteLine("Сообщение отправлено!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                finally
                {
                    // Отключаемся от сервера
                    await client.DisconnectAsync(true);
                }
            }

        }
    }
}