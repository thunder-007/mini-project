using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using BusBookingSystem.Interfaces;
using BusBookingSystem.Dtos;
using BusBookingSystem.Services;
using Microsoft.AspNetCore.Authentication;

namespace WebApi.Controllers;

[Route("/api/jwt")]
[ApiController]
public class JwtController(IJwtService service, ILogger<JwtController> logger) : ControllerBase
{
    private readonly IJwtService _service = service;
    private readonly ILogger<JwtController> _logger = logger;

    [HttpPost("generate-token")]
    [ProducesResponseType(typeof(JwtGetDto), StatusCodes.Status200OK)]
    public IActionResult GenerateToken([FromBody] LoginDto loginDto)
    {
        try
        {
            // Validate the user credentials here (this could be a call to a user service)
            // For the sake of simplicity, let's assume the validation is successful if username and password are not empty
            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                throw new AuthenticationFailureException("Invalid username or password.");
            }

            // Assuming a method ValidateUser exists in your service
            var isValidUser = _service.ValidateUser(loginDto.Email, loginDto.Password);
            if (!isValidUser)
            {
                throw new AuthenticationFailureException("Invalid username or password.");
            }
            
            var role = "User";

            _logger.LogInformation("Generating a new JWT token.");

            return Ok(new JwtGetDto
            {
                Token = _service.GenerateToken(loginDto.Email, role)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate a new JWT token.");
            return BadRequest(new ErrorResponseDto
            {
                Message = ex.Message,
                ErrorCode = "TokenGenerationFailed"
            });
        }
    }
}
