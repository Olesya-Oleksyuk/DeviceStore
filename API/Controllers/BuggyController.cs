using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  /// <summary>
  /// To set up some erros in order to see the different kinds of responses
  /// that's our API will return when it encounters certain types of errors.
  /// </summary>
  public class BuggyController : BaseApiController
  {

    private readonly StoreContext _context;
    public BuggyController(StoreContext context)
    {
      _context = context;
    }


    // Each of these requests generates a certain type of error:

    /// <summary>
    /// NotFound Error. Returns Status 404 NotFound response 
    /// </summary>
    [HttpGet("notfound")]
    public ActionResult GetNotFoundRequest()
    {
      var thing = _context.Products.Find(42);
      if (thing == null)
      {
        return NotFound(new ApiResponse(404));
      }
      return Ok();
    }

    /// <summary>
    /// Server Exception: Null Reference Exception 
    /// </summary>
    [HttpGet("servererror")]
    public ActionResult GetServerError()
    {
      var thing = _context.Products.Find(42);
      // trying to execute a method on smth that is null
      var thingToReturn = thing.ToString();
      return Ok();
    }

    /// <summary>
    /// Returns Status 400 BadRequest response.
    /// </summary>
    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
      return BadRequest(new ApiResponse(400));
    }

    /// <summary>
    /// Returns Status 400 BadRequest response related to validation.
    /// Validation kind of error.
    /// Returns an Errors object: key + message  
    /// </summary>
    [HttpGet("badrequest/{id}")]
    public ActionResult GetNotFoundRequest(int id)
    {
      // passing a string instead of an integer for the id. 
      return Ok();
    }


  }
}