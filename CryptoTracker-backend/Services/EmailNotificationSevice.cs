using CryptoTracker_backend.DTOs;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;


namespace CryptoTracker_backend.Services
{
    public class EmailNotificationSevice : INotificationService
    {
        private readonly IConfiguration _configuration;

        public EmailNotificationSevice(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Notify(EmailDTO request)
        {
       
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(
                _configuration.GetSection("Email:UserName").Value));
            email.To.Add(MailboxAddress.Parse(request.UserEmail));
            email.Subject = request.Asunt;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = request.Content
            };
          

            using (var smtp = new SmtpClient())
            {
                smtp.CheckCertificateRevocation = false;
                smtp.Connect(
                _configuration.GetSection("Email:Host").Value,
                Convert.ToInt32(_configuration.GetSection("Email:Port").Value),
            SecureSocketOptions.StartTls
                );


            smtp.Authenticate(
                _configuration.GetSection("Email:UserName").Value,
                _configuration.GetSection("Email:Password").Value);


            smtp.Send(email);
            smtp.Disconnect(true);
            }
        }
    }
}
