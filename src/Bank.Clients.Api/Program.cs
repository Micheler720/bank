using Bank.Clients.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddApiConfig(configuration);
services.AddSwaggerConfiguration();
services.AddMessageConfig();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseApiConfig();

app.Run();

