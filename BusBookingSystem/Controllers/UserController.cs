using BusBookingSystem.Dtos;
using BusBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using BusBookingSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BusBookingSystem.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (_userService.GetUserByEmail(userDto.Email) != null)
            return Conflict("Email is already registered.");

        _userService.RegisterUser(userDto);

        return Ok("User registered successfully.");
    }
}
