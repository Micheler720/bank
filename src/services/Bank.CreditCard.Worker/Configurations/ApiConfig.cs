using System.Diagnostics.CodeAnalysis;
using Bank.Clients.Api.Attributes;

namespace Bank.CreditCard.Worker.Configurations;

[ExcludeFromCodeCoverage]
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

        return services;
    }

    public static void UseApiConfiguration(this IApplicationBuilder app)
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