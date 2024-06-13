using System;

namespace BusBookingSystem.Exceptions
{
    public class TimeConflictException : Exception
    {
        public TimeConflictException(string message) : base(message)
        {   
            
        }
    }
}