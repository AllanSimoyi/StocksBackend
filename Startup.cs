public class Startup
{
  public void ConfigureServices(IServiceCollection services)
  {
    services.AddCors(); // Make sure you call this previous to AddMvc
                        // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
  }

  public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env, ILoggerFactory loggerFactory)
  {
    // Make sure you call this before calling app.UseMvc()
    app.UseCors(
        options => options.WithOrigins("http://localhost:4200").AllowAnyMethod()
    );

    // app.UseMvc();
  }
}