using Foodie.Application;
using Foodie.Core;
using Foodie.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCore();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseInfrastructure();
app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
