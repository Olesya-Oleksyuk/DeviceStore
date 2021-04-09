using System;

namespace API.Errors
{
  /// <summary>
  /// We utilize this class when we're returning an error response inside the controller.

  /// </summary>
  public class ApiResponse
  {
    // When we create a new instance of ApiResponse, we can pass in the props that we need.
    public ApiResponse(int statusCode, string message = null)
    {
      StatusCode = statusCode;
      // Null coalesing operator 
      Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }


    // Every Error-Response is gonna gave at least these 2 props.
    public int StatusCode { get; set; }
    public string Message { get; set; }

    /// <summary>
    /// Based on the status code that we're passing in, we're gonna return our own cusome 
    /// messages, so that each type of error has at least a message.
    /// </summary>
    private string GetDefaultMessageForStatusCode(int statusCode)
    {
      // Switch expression from C# ver.8
      return statusCode switch
      {
        400 => "You've made a Bad Request!",
        401 => "You're not Authorized!",
        404 => "Resource was not found!",
        500 => "Server Error has occurred!",
        _ => null
      };
    }
  }

}