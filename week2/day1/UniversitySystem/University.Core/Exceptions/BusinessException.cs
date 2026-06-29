namespace University.Core.Exceptions;

using System;
using System.Collections.Generic;

public class BusinessException : Exception
{
    public Dictionary<string, List<string>> Errors { get; }

    public BusinessException(string message) : base(message)
    {
        Errors = new Dictionary<string, List<string>>();
    }

    public BusinessException(Dictionary<string, List<string>> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors ?? new Dictionary<string, List<string>>();
    }
}