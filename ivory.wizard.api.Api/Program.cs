using ivory.wizard.api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Ivory.Wizard.Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

string authority = builder.Configuration["Auth0:Authority"] ??
throw new ArgumentNullException("Auth0:Authority");

string audience = builder.Configuration["Auth0:Audience"] ??
throw new ArgumentNullException("Auth0:Audience");


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
options.Authority = authority;
options.Audience = audience;
});
builder.Services.AddAuthorization(options =>
{
options.AddPolicy("delete:catalog", policy =>
policy.RequireAuthenticatedUser().RequireClaim("scope", "delete:catalog"));
});
builder.Services.AddDbContext<StoreContext>(options => options.UseSqlite("Data Source =..Registrar.sqlite", b => b.MigrationsAssembly("ivory.wizard.api.Api")));

builder.Services.AddCors(options =>
                         {
                             options.AddDefaultPolicy(builder =>
                                                      {
                                                          builder.WithOrigins("http://localhost:3000")
                                                              .AllowAnyHeader()
                                                              .AllowAnyMethod():
                                                      });
                         });

builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");


app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
