using aspnet_mongo.Validations;
using System.ComponentModel.DataAnnotations;

namespace aspnet_mongo.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Endereço de email inválido")]
        [UniqueEmail(ErrorMessage = "Endereço de email já cadastrado")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [PasswordComplexity(ErrorMessage = "A senha deve conter no mínimo 8 caracteres, 1 letra maiúscula e 1 caractere especial!")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string? ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nome completo")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Nome de usuário")]
        public string? UserName { get; set; }

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
