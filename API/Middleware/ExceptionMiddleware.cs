using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
  /// <summary>
  /// Custom Middleware to handle Server Exceptions (500 status code);
  /// Return: Json formatted response;
  /// </summary>
  public class ExceptionMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
    IHostEnvironment env)
    {
      _env = env;
      _logger = logger;
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        // output into the logging system (i.e. the console)
        _logger.LogError(ex, ex.Message);
        // all of our responses gonna be sent as Json formatted responses
        context.Response.ContentType = "application/json";
        // set the status code to be a 500 internal server error
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // Different responses for development and production modes (more or less info).  
        var response = _env.IsDevelopment()
            ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
            : new ApiException((int)HttpStatusCode.InternalServerError);

        // ensures that the returned response policy will be Camel Case. 
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);

      }
    }
  }
}