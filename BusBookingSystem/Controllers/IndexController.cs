using Microsoft.AspNetCore.Mvc;

namespace BusBookingSystem.Controllers;

public class IndexController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Bus Booking API up and running!");
    }
}