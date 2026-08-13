namespace projetobiblioteca.Mail.Data
{
    public class EmailData
    {
        public string Host { get; set; }
        public int Port {  get; set; }
        public string Username {  get; set; }
        public string Password {  get; set; }
        public string From {  get; set; }
        public string Subject { get; set; }
        public string Message {  get; set; }
        public bool Ssl {  get; set; }
        public MailData Properties { get; set; }
        = new();
    }
}
