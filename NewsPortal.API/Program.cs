using Microsoft.EntityFrameworkCore;
using NewsPortal.API.Extensions;
using NewsPortal.API.Middleware;
using NewsPortal.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();
builder.Services.AddEFCoreRepositories(connectionString!);
builder.Services.AddServices();
builder.Services.AddMappings();
builder.Services.AddValidators();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
