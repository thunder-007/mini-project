using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

public class CreateBookingDtoTests
{
    [Fact]
    public void CreateBookingDto_ShouldPassValidation_WhenAllFieldsAreValid()
    {
        // Arrange
        var createBookingDto = new CreateBookingDto
        {
            BusId = 1,
            BookingDate = DateTime.Now,
            SeatNumber = 10,
            CouponCode = "DISCOUNT10",
            Amount = 100.00m,
            PaymentMethod = "CreditCard"
        };

        // Act
        var validationResults = ValidateModel(createBookingDto);

        // Assert
        Assert.Empty(validationResults);
    }

    [Fact]
    public void CreateBookingDto_ShouldFailValidation_WhenRequiredFieldsAreMissing()
    {
        // Arrange
        var createBookingDto = new CreateBookingDto();

        // Act
        var validationResults = ValidateModel(createBookingDto);

        // Assert
        Assert.NotEmpty(validationResults);
    }

    [Fact]
    public void CreateBookingDto_ShouldFailValidation_WhenPaymentMethodExceedsMaxLength()
    {
        // Arrange
        var createBookingDto = new CreateBookingDto
        {
            BusId = 1,
            BookingDate = DateTime.Now,
            SeatNumber = 10,
            CouponCode = "DISCOUNT10",
            Amount = 100.00m,
            PaymentMethod = new string('A', 21) // Exceeds max length of 20
        };

        // Act
        var validationResults = ValidateModel(createBookingDto);

        // Assert
        Assert.NotEmpty(validationResults);
    }

    private IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, validationContext, validationResults, true);
        return validationResults;
    }
}