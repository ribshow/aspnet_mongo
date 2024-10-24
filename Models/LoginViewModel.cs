using System.ComponentModel.DataAnnotations;

namespace aspnet_mongo.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo email deve ser preenchido!")]
        [EmailAddress(ErrorMessage = "Endereço de email inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo senha deve ser preenchido!")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string? Password { get; set; }

        [Display(Name = "Lembrar a senha?")]
        public bool RememberMe { get; set; } = false;
    }
}
