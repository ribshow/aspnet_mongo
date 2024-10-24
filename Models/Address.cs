using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspnet_mongo.Models
{
    [Table("Address")]
    public class Address
    {
        [Column("Id")]
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        [Column("UserId")]
        [Display(Name = "Usuário")]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [Column("Street")]
        [Display(Name = "Logradouro")]
        public string? Street { get; set; }

        [Column("Number")]
        [Display(Name = "Número")]
        public string? Number { get; set; }

        [Column("Cep")]
        [Display(Name = "CEP")]
        public string? Cep { get; set; }
    }
}
