namespace API.Errors
{
  public class ApiException : ApiResponse
  {
    public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
    {
      Details = details;
    }

    /// <summary>
    /// Contains the "Stack trace".
    /// </summary>
    public string Details { get; set; }
  }
}