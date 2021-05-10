using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
  public static class SwaggerServiceExtenstions
  {
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "DeviceStore API ", Version = "v1" });
      });

      return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
      // allows us to browse to a web page, which is gonna show all of our API endpoints
      app.UseSwagger();
      // set up a URL where put the Swagger endpoint is gonna be located
      app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeviceStore API v1"); });

      return app;
    }
  }
}
