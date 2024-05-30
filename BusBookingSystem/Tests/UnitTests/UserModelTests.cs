using Xunit;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using BusBookingSystem.Models;

namespace BusBookingSystem.Tests
{
    public class UserModelTests
    {
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void UserName_ShouldNotExceedMaxLength()
        {
            
            var user = new User
            {
                UserName = new string('a', 51), 
                PasswordHash = "password",
                Email = "test@example.com",
                Role = "User"
            };

            
            var results = ValidateModel(user);

            
            Assert.Single(results);
            Assert.Equal("The field UserName must be a string or array type with a maximum length of '50'.", results[0].ErrorMessage);
        }

        [Fact]
        public void Email_ShouldBeAValidEmailAddress()
        {
            
            var user = new User
            {
                UserName = "TestUser",
                PasswordHash = "password",
                Email = "invalid-email",
                Role = "User"
            };

            
            var results = ValidateModel(user);

            
            Assert.Single(results);
            Assert.Equal("The Email field is not a valid e-mail address.", results[0].ErrorMessage);
        }

        [Fact]
        public void Role_ShouldNotExceedMaxLength()
        {
            
            var user = new User
            {
                UserName = "TestUser",
                PasswordHash = "password",
                Email = "test@example.com",
                Role = new string('a', 21) 
            };

            
            var results = ValidateModel(user);

            
            Assert.Single(results);
            Assert.Equal("The field Role must be a string or array type with a maximum length of '20'.", results[0].ErrorMessage);
        }
    }
}
