namespace projetobiblioteca.Data.DTO.User
{
    public record RegisterUser(string Username, string Fullname,
        string PasswordHash,string Email);
    
}
