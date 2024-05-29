using Microsoft.AspNetCore.Mvc;
using BusBookingSystem.Dtos;
using BusBookingSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BusBookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]

    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet("{id}")]
        public IActionResult GetRouteById(int id)
        {
            var route = _routeService.GetRouteById(id);
            if (route == null) return NotFound();

            return Ok(route);
        }

        [HttpGet]
        public IActionResult GetAllRoutes()
        {
            var routes = _routeService.GetAllRoutes();
            return Ok(routes);
        }

        [HttpPost]
        public IActionResult AddRoute([FromBody] CreateRouteDto createRouteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdRoute = _routeService.AddRoute(createRouteDto);
            return CreatedAtAction(nameof(GetRouteById), new { id = createdRoute.RouteId }, createdRoute);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoute(int id, [FromBody] UpdateRouteDto updateRouteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _routeService.UpdateRoute(id, updateRouteDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoute(int id)
        {
            _routeService.DeleteRoute(id);
            return NoContent();
        }
    }
}