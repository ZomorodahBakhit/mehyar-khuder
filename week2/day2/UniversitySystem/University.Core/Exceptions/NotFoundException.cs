namespace University.Core.Exceptions;

using System;

public class NotFoundException : Exception
    {
        public NotFoundException() : this("Requested record not found.")
        {
        }
        public NotFoundException(string message) : base(message)
        {
        }
    }
