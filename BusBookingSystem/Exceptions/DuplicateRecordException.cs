﻿using System;

namespace BusBookingSystem.Exceptions
{
    public class DuplicateRecordException : Exception
    {
        public DuplicateRecordException(string message) : base(message)
        {
        }
    }
}