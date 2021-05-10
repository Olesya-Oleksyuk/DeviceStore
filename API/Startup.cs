using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using API.Helpers;
using API.Middleware;
using API.Extensions;

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

      services.AddApplicationServices();

      services.AddAutoMapper(typeof(MappingProfiles));

      services.AddSwaggerDocumentation();


    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // REPLACED with our own Exception Handling Middleware !!!
      // Exception Handling option #1: Error Handling applying to whether or not we're in development.
      // Use the Developer Exception Page. 
      if (env.IsDevelopment())
      {
        // app.UseDeveloperExceptionPage();

        // Exception Handling option #2: custom Exception Handling Middleware.
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseStatusCodePagesWithReExecute("/errors/{0}");

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseStaticFiles();

        app.UseAuthorization();

        app.UseSwaggerDocumentation();

        app.UseEndpoints(endpoints =>
        {
          endpoints.MapControllers();
        });
      }
    }
  }
}

