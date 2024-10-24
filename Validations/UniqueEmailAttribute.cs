using aspnet_mongo.Models;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace aspnet_mongo.Validations
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        //private readonly ContextMongoDb _context;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var _context = (ContextMongoDb)validationContext.GetService(typeof(ContextMongoDb));

            var email = value?.ToString();
            var filter = Builders<ApplicationUser>.Filter.Eq("Email", email);
            bool emailExists = _context.User.Find(filter).Any();
            
            if(emailExists)
            {
                return new ValidationResult("O email já está em uso.");
            }
            return ValidationResult.Success;
        }
   
    }
}
