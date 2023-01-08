var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Add services to the container.

app.UseAuthorization();

app.MapControllers();

app.Run();
