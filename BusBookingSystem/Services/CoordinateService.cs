using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using BusBookingSystem.Models;

namespace BusBookingSystem.Services
{
    public class CoordinateService
    {
        private static readonly Random _random = new Random();

        public async Task WriteRandomCoordinatesAsync(ChannelWriter<RouteCoordinate> writer)
        {
            while (await writer.WaitToWriteAsync())
            {
                var coordinate = new RouteCoordinate
                {
                    Latitude = _random.NextDouble() * 180 - 90,
                    Longitude = _random.NextDouble() * 360 - 180
                };
                await writer.WriteAsync(coordinate);
                await Task.Delay(1000);
            }
            writer.Complete();
        }
    }
}