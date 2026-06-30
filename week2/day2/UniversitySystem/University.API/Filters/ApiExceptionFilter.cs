namespace University.API.Filters;

using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using University.Core.Exceptions;

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;

    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is NotFoundException)
        {
            context.Result = Response(context.Exception.Message, "Item not found", StatusCodes.Status404NotFound);
            context.ExceptionHandled = true;
            return;
        }

        if (context.Exception is BusinessException businessException)
        {
            _logger.LogWarning("Validation failed. Errors: {@Message}", businessException.Message);

            if (businessException.Errors != null && businessException.Errors.Any())
                context.Result = Response(businessException.Errors, "One or more business validation errors occurred.", StatusCodes.Status400BadRequest);
            else
                context.Result = Response(businessException.Message, "One or more business validation errors occurred.", StatusCodes.Status400BadRequest);

            context.ExceptionHandled = true;
            return;
        }

        if (context.Exception is ArgumentNullException)
        {
            _logger.LogError(context.Exception, "ArgumentNullException occurred.");
            context.Result = Response(context.Exception.Message, "Missing data", StatusCodes.Status400BadRequest);
            context.ExceptionHandled = true;
            return;
        }

        if (context.Exception is UnauthorizedAccessException)
        {
            _logger.LogError(context.Exception, "UnauthorizedAccessException occurred.");
            context.Result = Response(context.Exception.Message, "Unauthorized", StatusCodes.Status403Forbidden);
            context.ExceptionHandled = true;
            return;
        }

        _logger.LogError(context.Exception, "Unhandled exception occurred");

        context.Result = Response("An unexpected error occurred.", "Internal Server Error", StatusCodes.Status500InternalServerError, context.Exception.StackTrace);
        context.ExceptionHandled = true;
    }

    private ObjectResult Response(string message, string title, int status, string? stackTrace = null)
    {
        var result = new ApiResponse
        {
            StatusCode = status,
            Message = message,
            ResponseException = title,
            IsError = true,
            Version = "1.0",
            Result = stackTrace
        };

        return new ObjectResult(result) { StatusCode = status };
    }

    private ObjectResult Response(Dictionary<string, List<string>> errors, string title, int status)
    {
        var result = new ApiResponse
        {
            StatusCode = status,
            Message = title,
            ResponseException = title,
            IsError = true,
            Version = "1.0",
            Result = errors
        };

        return new ObjectResult(result) { StatusCode = status };
    }
}