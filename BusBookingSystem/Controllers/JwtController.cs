using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using BusBookingSystem.Interfaces;
using BusBookingSystem.Dtos;
using BusBookingSystem.Services;
using Microsoft.AspNetCore.Authentication;

namespace BusBookingSystem.Controllers;

[Route("/api/auth")]
[ApiController]
public class JwtController : ControllerBase
{
    private readonly IJwtService _service;
    private readonly IUserService _userService;
    private readonly ILogger<JwtController> _logger;

    public JwtController(IJwtService service, IUserService userService, ILogger<JwtController> logger)
    {
        _service = service;
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("jwt")]
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

            var user = _userService.GetUserByEmail(loginDto.Email);
            var role = user.Role;

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