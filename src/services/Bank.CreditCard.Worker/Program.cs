using Bank.CreditCard.Worker.Configurations;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddApiConfig(builder.Configuration);
services.AddDatabaseConfiguration();
services.AddSwaggerConfiguration();
services.AddDependecyResolver();
services.AddLogConfig();
services.AddMessageConfig();

var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseApiConfiguration();

app.Run();


