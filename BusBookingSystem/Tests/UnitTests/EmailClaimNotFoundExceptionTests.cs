using System;
using System.Threading.Tasks;
using Xunit;

public class EmailClaimNotFoundExceptionTests
{
    [Fact]
    public async Task EmailClaimNotFoundException_ShouldThrowExceptionAsync()
    {
        // Arrange
        var expectedMessage = "Email claim not found";

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EmailClaimNotFoundException>(async () => 
        {
            await Task.Run(() => throw new EmailClaimNotFoundException(expectedMessage));
        });
        Assert.Equal(expectedMessage, exception.Message);
    }
}