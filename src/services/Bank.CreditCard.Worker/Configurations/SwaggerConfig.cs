using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;

namespace Bank.CreditCard.Worker.Configurations;

[ExcludeFromCodeCoverage]
public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Bank Credit Card",
                Description = "Api responsável pelo cadastro de cartões de crédito",
                Contact = new OpenApiContact() { Email = "micheler720@gmail.com", Name = "Michele de Freitas" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/license/MIT") }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bank.CreditCard v1");
        });
        return app;
    }

}