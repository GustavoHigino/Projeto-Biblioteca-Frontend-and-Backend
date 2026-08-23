
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using projetobiblioteca.Mail.Data;
using Serilog;
namespace projetobiblioteca.Mail


{
    public class EmailSender
    {
        private readonly EmailData _data;
        private readonly ILogger<EmailSender> _logger;
        private string _to;
        private string _subject;
        private string _body;
        private readonly List<MailboxAddress>
            _recepients = new();
        private string _attachment;
        public EmailSender(ILogger<EmailSender> logger,
            EmailData data)
        {
            _data = data;
            _logger = logger;
        }
        public EmailSender To(string to)
        {
            _to = to;
            _recepients.Clear();
            _recepients.AddRange(
                ParseRecipients(to));
            return this;
        }
        public EmailSender WithSubject(string subject)
        {
            _subject= subject;
            return this;
        }
        public EmailSender WithMessage(string body)
        {
            _body = body;
            return this;
        }
        public EmailSender Attach(string filePath)
        {
            if(File.Exists(filePath))
            {
                _attachment = filePath;
                return this;
            }
            else
            {
                _logger.LogWarning("attachment file" +
                    "not found verify your path again");
            }
            return this;
        }
        public void Send()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress
                (_data.From, _data.Username));
            message.To.AddRange(_recepients);
            message.Subject = _subject ??
                _data.Subject ?? "No subject";
            var builder = new BodyBuilder
            {
                HtmlBody = _body ?? _data.Message ?? ""
            };
            if(!string.IsNullOrWhiteSpace
                (_attachment))
            {
                var fileName = Path.GetFileName
                    (_attachment);
                builder.Attachments
                    .Add(fileName, File
                    .ReadAllBytes(_attachment));
            }
            message.Body = builder.ToMessageBody();
            try
            {
                using var client = new SmtpClient();
                client.Connect(_data.Host,
                    _data.Port,
                    _data.Ssl ?
                    SecureSocketOptions.StartTls :
                    SecureSocketOptions.None);
                client.Authenticate(_data.Username,
                    _data.Password);
                client.Send(message);
                client.Disconnect(true);
                _logger.LogInformation($"Email send" +
                    $"successfully to {string.Join(
                        ";", _recepients)}");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Error" +
                    $" to send an email try again," +
                    $"{string.Join(";", _recepients)}");
            }

            finally
            {
                Reset();
            }
        }

        private void Reset()
        {
            _to = null;
            _subject = null;
            _body = null;
            _recepients.Clear();
            _attachment = null;
        }

        private IEnumerable<MailboxAddress> ParseRecipients(string to)
        {
            var toWithouthSpace = to.Replace(" ",
                string.Empty);
            var recepients = toWithouthSpace
                .Split(';',
                StringSplitOptions.RemoveEmptyEntries);
            var list = new List<MailboxAddress>();
            foreach(var address in recepients)
            {
                try
                {
                    var mailbox=MailboxAddress
                        .Parse(address);
                    list.Add(mailbox);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex,
                        $"Invalid e-mail address" +
                        $": {address}");
                }
            }
            return list;
        }
    }
}
