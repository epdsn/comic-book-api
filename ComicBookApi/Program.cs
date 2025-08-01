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
var jwtKey = "your_super_secret_jwt_key_that_is_at_least_32_characters_long_for_hs256"; // At least 32 chars for HS256
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
                          policy.WithOrigins(
                              "https://localhost:4200",  // Angular default HTTPS
                              "http://localhost:4200",   // Angular default HTTP
                              "https://localhost:4201",  // Alternative Angular port
                              "http://localhost:4201"    // Alternative Angular port
                          )
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials()
                          .WithExposedHeaders("Content-Disposition"); // For file downloads if needed
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

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
