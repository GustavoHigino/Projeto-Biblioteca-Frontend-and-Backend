using projetobiblioteca.Mail.Dto;

namespace projetobiblioteca.Servicos
{
    public interface IEmailService
    {
        void SendSimpleEmail(EmailRequestDTO dto);
        Task SendEmailWithAttachment ( EmailRequestDTO dto,
            IFormFile attachment);
    }
}
