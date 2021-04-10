using System.Collections.Generic;

namespace API.Errors
{
  public class ApiValidationErrorResponse : ApiResponse
  {
      // use the status code that we know that we're gonna send from this, which is gonna be a 400.
    public ApiValidationErrorResponse() : base(400)
    {
    }
    public IEnumerable<string> Errors { get; set; }
  }
}