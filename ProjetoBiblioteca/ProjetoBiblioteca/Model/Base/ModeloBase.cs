using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projetobiblioteca.Model.Base
{
    public class ModeloBase
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(
            DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }
}
