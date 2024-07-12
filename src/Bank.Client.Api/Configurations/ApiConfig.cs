using System.Reflection;
using Bank.Client.Api.Attributes;
using Serilog;

namespace Bank.Client.Api.Configurations;
public static class ApiConfig
{
    public static IServiceCollection AddApiConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMvc(options => options.Filters.Add(new DefaultExceptionFilterAttribute()));

        services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });    

        services.AddCors();       
        services.AddControllers();

        var mediatorConfiguration = new MediatRServiceConfiguration()
            .RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            
        services.AddMediatR(mediatorConfiguration);

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        return services;
    }

    public static void UseApiConfig(this IApplicationBuilder app)
    {
        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()
        );

        app.UseRouting();

       
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}