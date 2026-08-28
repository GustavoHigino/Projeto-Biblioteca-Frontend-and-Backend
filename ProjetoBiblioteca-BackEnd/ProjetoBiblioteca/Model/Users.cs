using Microsoft.EntityFrameworkCore;
using projetobiblioteca.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projetobiblioteca.Model
{
    [Table("Users")]
    [Index(nameof(Username),IsUnique =true)]
    public class Users : ModeloBase
    {
        [Required]
        [MaxLength(100)]
        [Column("Username",TypeName ="varchar(100)")]
        public string Username {  get; set; }
        [Required]
        [MaxLength(100)]
        [Column("Fullname",TypeName ="varchar(100)")]
        public string Fullname { get; set; }
        [Required]
        [MaxLength(300)]
        [Column("passwordhash",TypeName ="varchar(300)")]
        public string PasswordHash {  get; set; }
        
        [MaxLength(500)]
        [Column("refreshtoken",TypeName ="varchar(300)")]
        public string? RefreshToken { get; set; }
        
        
        [Column("refreshtokenexpirytime",
            TypeName ="datetime2")]
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public bool Enable { get; set; } = false;
        [MaxLength(500)]
        [Column("Key",TypeName ="varchar(500)")]
        public string Key { get; set; }
        [Required]
        [MaxLength(150)]
        [EmailAddress]
        [Column("email",TypeName ="varchar(150)")]
        public string Email { get; set; }


    }
}
