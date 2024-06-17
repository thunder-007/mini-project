using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BusBookingSystem.Dtos;
using BusBookingSystem.Interfaces;

namespace BusBookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;

        public BusController(IBusService busService)
        {
            _busService = busService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BusDto>> GetAllBuses()
        {
            var buses = _busService.GetAllBuses();
            return Ok(buses);
        }

        [HttpGet("{id}")]
        public ActionResult<BusDto> GetBusById(int id)
        {
            var bus = _busService.GetBusById(id);
            if (bus == null)
            {
                return NotFound();
            }
            return Ok(bus);
        }

        [HttpPost]
        public ActionResult AddBus([FromBody] CreateBusDto createBusDto)
        {
            var createdBus = _busService.AddBus(createBusDto);
            return CreatedAtAction(nameof(GetBusById), new { id = createdBus.BusId }, createdBus);
        }


        [HttpPut("{id}")]
        public ActionResult UpdateBus(int id, [FromBody] BusDto busDto)
        {
            if (id != busDto.BusId)
            {
                return BadRequest();
            }

            _busService.UpdateBus(busDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBus(int id)
        {
            var bus = _busService.GetBusById(id);
            if (bus == null)
            {
                return NotFound();
            }

            _busService.DeleteBus(id);
            return NoContent();
        }
        [HttpGet("search")]
        public ActionResult<IEnumerable<BusDto>> SearchBuses([FromQuery] string source, [FromQuery] string destination)
        {
            var buses = _busService.SearchBuses(source, destination);
            return Ok(buses);
        }

    }
}