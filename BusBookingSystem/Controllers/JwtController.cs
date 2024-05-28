using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using BusBookingSystem.Interfaces;
using BusBookingSystem.Dtos;
using BusBookingSystem.Services;
using Microsoft.AspNetCore.Authentication;

namespace BusBookingSystem.Controllers;

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
            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                throw new AuthenticationFailureException("Invalid username or password.");
            }

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
