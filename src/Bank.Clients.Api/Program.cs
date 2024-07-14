using Bank.Clients.Api.Configurations;
using Bank.Message;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddApiConfig(configuration);
services.AddSwaggerConfiguration();
services.AddMessageBus();
services.AddDependecyResolver();
services.AddDatabaseConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseApiConfig();

app.Run();

