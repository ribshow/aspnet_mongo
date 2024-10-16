using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspnet_mongo.Models
{
    [Table("User")]
    public class User
    {

        // representa um identificador global único
        [Column("Id")]
        [Display(Name = "Identificador")]
        public Guid Id {  get; set; }
        [Column("Name")]
        [Display(Name = "Nome")]
        public string? Name { get; set; }

        [Column("Age")]
        [Display(Name = "Idade")]
        public int Age { get; set; }

        [Column("Imagem_url")]
        [Display(Name = "Imagem")]
        public string? Image_url { get; set; }
    }
}
