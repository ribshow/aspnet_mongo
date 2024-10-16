using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspnet_mongo.Models
{
    [Table("Product")]
    public class Product
    {
        [Column("Id")]
        [Display(Name = "Identificador")]
        public Guid Id { get; set; }
        [Column("Name")]
        [Display(Name = "Nome")]
        public string? Name { get; set; }
        [Column("Price")]
        [Display(Name = "Preço")]
        public double? Price { get; set; }
        [Column("Description")]
        [Display(Name="Descrição")]
        public string? Description { get; set; }

        [Column("Image_url")]
        [Display(Name ="Imagem")]
        public string? Image_url { get; set; }

    }
}
