using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AutoMapper;
using API.Helpers;
using API.Middleware;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using API.Errors;

namespace API
{
  public class Startup
  {
    private readonly IConfiguration _config;
    public Startup(IConfiguration config)
    {
      _config = config;
    }


    // Чуваку из видео не понравилось, как мы инджектим по-умолчанию, поэтому он удалил строку ниже
    // public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));
      services.AddScoped<IProductRepository, ProductRepository>();
      // register service which uses Generics
      services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
      services.AddAutoMapper(typeof(MappingProfiles));

      // brings 400 Validation Error into line with the other error responses (uses custom error response view)
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

      // services.AddSwaggerGen(c =>
      // {
      //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
      // });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // // REPLACED with our own Exception Handling Middleware !!!
      // // Exception Handling option #1: Error Handling applying to whether or not we're in development.
      // // Use the Developer Exception Page. 
      // if (env.IsDevelopment())
      // {
      //   app.UseDeveloperExceptionPage();
      //   // app.UseSwagger();
      //   // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
      // }

      // Exception Handling option #2: custom Exception Handling Middleware.
      app.UseMiddleware<ExceptionMiddleware>();

      app.UseStatusCodePagesWithReExecute("/errors/{0}");

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseStaticFiles();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
