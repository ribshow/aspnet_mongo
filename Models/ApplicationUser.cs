using AspNetCore.Identity.MongoDbCore;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;

namespace aspnet_mongo.Models
{
    [CollectionName("User")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        [Required]
        [Display(Name = "Nome completo")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Idade")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Gênero")]
        public Gender Gender { get; set; }

        [Display(Name = "Imagem")]
        public string? Image_url { get; set; } = null;
    }
}
