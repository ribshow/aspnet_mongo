using System.ComponentModel.DataAnnotations;

namespace aspnet_mongo.Validations
{
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var password = value as string;

            if(string.IsNullOrWhiteSpace(password))
            {
                return new ValidationResult("A senha é obrigatória!");
            }

            // verificar o tamanho da senha
            if(password.Length < 8)
            {
                return new ValidationResult("A senha deve conter pelo menos 8 caracteres!");
            }

            // verificar se há pelo menos uma letra maiúscula
            if(!password.Any(char.IsUpper))
            {
                return new ValidationResult("A senha deve conter ao menos 1 caractere maiúsculo!");
            }

            // verificar se há pelo menos um caractere especial
            if(!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return new ValidationResult("A senha deve conter ao menos 1 caractere especial!");
            }

            return ValidationResult.Success;
        }
    }
}
