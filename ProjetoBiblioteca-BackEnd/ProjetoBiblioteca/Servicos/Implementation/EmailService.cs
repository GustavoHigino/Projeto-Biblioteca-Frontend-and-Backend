using projetobiblioteca.Mail;
using projetobiblioteca.Mail.Dto;

namespace projetobiblioteca.Servicos.Implementation
{
    public class EmailService : IEmailService

    {
        private readonly ILogger<EmailService> _logger;
        private readonly EmailSender _email;
        public EmailService(ILogger<EmailService> logger,
            EmailSender email)
        {
            _logger = logger;
            _email = email;
        }
        public void SendSimpleEmail(EmailRequestDTO dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("failure in " +
                    "send an email without attach ");

                throw new ArgumentException("failure in " +
                    "send an email without attach ");
            }
            _email.To(dto.To)
                .WithSubject(dto.Subject)
                .WithMessage(dto.Body)
                .Send();
        }
        public async Task SendEmailWithAttachment
            (EmailRequestDTO dto, IFormFile attachment)
        {
            if (dto == null)
            {
                _logger.LogWarning("failure in " +
                    "send an email without attach ");
                throw new ArgumentException("failure in " +
                    "send an email without attach ");
            }
            if (attachment==null|| attachment.Length == 0)
            {
                _logger.LogWarning("Attachment is" +
                    " null or empty");
                throw new ArgumentException("Attach" +
                    "ment is null or empty");
            }
            string tempFilePath = Path.Combine
                (Path.GetTempPath(),
                attachment.FileName);
            try
            {
                await using (var stream = new 
                    FileStream
                    (tempFilePath,
                    FileMode.Create))
                {
                    await attachment.CopyToAsync
                        (stream);
                }
                _email.To(dto.To)
                    .WithSubject(dto.Subject)
                    .WithMessage(dto.Body)
                    .Attach(tempFilePath)
                    .Send();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex,
                    "Error sending an email with" +
                    " attachment");
            }
            finally
            {
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);                }
            }
        }

    }
}
