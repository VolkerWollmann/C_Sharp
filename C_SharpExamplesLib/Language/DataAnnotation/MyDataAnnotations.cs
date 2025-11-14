using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language.DataAnnotations
{
    public class MyDataAnnotations
    {
        private class User
        {
            [Required(ErrorMessage = "Name is required.")]
            [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
            public string Name { get; set; } = "";

            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email format.")]
            public string Email { get; set; } = "";
            [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
            public int Age { get; set; }
        }

        public static void Validate()
        {
            var user = new User
            {
                Name = "John Doe",
                Email = "invalid-email", // Invalid email format
                Age = 17 // Invalid age
            };
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user);
            bool isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            Assert.IsFalse(isValid);
            Assert.IsTrue(validationResults.Any(e => e.ErrorMessage == "Invalid email format."));
            Assert.IsTrue(validationResults.Any(e => e.MemberNames.Contains(nameof(User.Email))));

        }
    }
}
