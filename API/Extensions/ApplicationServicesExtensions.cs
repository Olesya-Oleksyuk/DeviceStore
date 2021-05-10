using System.Linq;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
  public static class ApplicationServicesExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      services.AddScoped<IProductRepository, ProductRepository>();
      // register service which uses Generics
      services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

      // brings 400 Validation Error respopnse into the line with the other error responses (uses custom error response view)
      services.Configure<ApiBehaviorOptions>(options =>
      {
        options.InvalidModelStateResponseFactory = ActionContext =>
        {
          // Getting "errors"-array of STRINGS,in which each of the string is ErrorMessage string. 
          // Note: Errors-objects are flattened out into an array of strings.
          var errors = ActionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage).ToArray();

          var errorResonse = new ApiValidationErrorResponse
          {
            Errors = errors
          };

          return new BadRequestObjectResult(errorResonse);
        };
      });

      return services;
    }



  }
}