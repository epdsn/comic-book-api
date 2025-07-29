using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

var AllowFrontend = "_allowFrontend";

//EF Core and Identity setup
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("ComicBookAuth")); // use InMemory for simplicity, replace with UseSqlServer for production

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
// JWT Settings
var jwtKey = "your_jwt_secret_key_123"; // Replace with your actual secret key
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowFrontend,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(AllowFrontend);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

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
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
