using Booking_Airline.DTOs;
using System.Net.Mail;

namespace Booking_Airline.Repository.EmailService
{
    public class EmailRepository : IEmailRepository
    {
        public readonly IConfiguration _config;

        public EmailRepository(IConfiguration config)
        {
            _config = config;

        }
        public void SendEmail(EmailDTO request)
        {

            MailMessage mail = new MailMessage();
            mail.To.Add(request.To);
            mail.From = new MailAddress(_config.GetSection("EmailHost").Value);
            mail.Subject = $"Verify code for {request.Subject} register";
            mail.Body = $"<p>Hello {request.Subject}</p><br/>This is your verify token </p><br/><h1>{request.Random}</h1>";
            mail.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {

                smtp.Port = 587; // 25 465
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new System.Net.NetworkCredential(_config.GetSection("EmailHost").Value, _config.GetSection("SecretAppPassword").Value);
                smtp.Send(mail);
            }
        }
    }
}
