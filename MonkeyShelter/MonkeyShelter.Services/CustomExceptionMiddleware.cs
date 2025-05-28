using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MonkeyShelter.Core.Domain_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;


namespace MonkeyShelter.Services
{
    /// <summary>
    /// Global exception handling middleware, for logging unhandled exceptions (with status code and error details on warning/error level).
    /// </summary>
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);//continue exception handling..
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occured.");

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        //Returns HttpResponse [ header, status code, json body {"error":   , "statusCode": } ]
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //header
            context.Response.ContentType = "application/json";

            //Add custom exceptions with status codes
            context.Response.StatusCode = exception switch
            {
                CustomNotFoundException => (int)HttpStatusCode.NotFound,
                DepartureNotAllowedException => (int)HttpStatusCode.Forbidden,
                MaxArrivalCountReachedExc => (int)HttpStatusCode.Conflict,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var response = new
            {
                error = exception.Message,
                statusCode = context.Response.StatusCode
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}
