using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  // override the routes that we get from our base API controller
  //   redirected to the ApiResponse.cs ???
  [Route("errors/{code}")]
  [ApiExplorerSettings(IgnoreApi = true)]
  public class ErrorController : BaseApiController
  {
    /// <summary>
    ///  Add a custom action method to generate and return an ApiResponse object. 
    /// </summary>
    public IActionResult Error(int code)
    {
      return new ObjectResult(new ApiResponse(code));
    }
  }
}