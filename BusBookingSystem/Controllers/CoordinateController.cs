using BusBookingSystem.Models;
using BusBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BusBookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoordinatesController : ControllerBase
    {
        private readonly CoordinateService _coordinateService;

        public CoordinatesController(CoordinateService coordinateService)
        {
            _coordinateService = coordinateService;
        }

        [HttpGet("stream-random-coordinates")]
        public IActionResult StreamRandomCoordinates()
        {
            var channel = Channel.CreateUnbounded<RouteCoordinate>();

            _ = _coordinateService.WriteRandomCoordinatesAsync(channel.Writer);

            return new PushStreamResult(channel.Reader);
        }
    }

    public class PushStreamResult : IActionResult
    {
        private readonly ChannelReader<RouteCoordinate> _channelReader;

        public PushStreamResult(ChannelReader<RouteCoordinate> channelReader)
        {
            _channelReader = channelReader;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.ContentType = "text/event-stream";

            await foreach (var coordinate in _channelReader.ReadAllAsync())
            {
                var data = $"data: {System.Text.Json.JsonSerializer.Serialize(coordinate)}\n\n";
                await context.HttpContext.Response.WriteAsync(data);
                await context.HttpContext.Response.Body.FlushAsync();
            }
        }
    }
}