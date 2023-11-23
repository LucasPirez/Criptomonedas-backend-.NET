using CryptoTracker_backend.Alerts;
using CryptoTracker_backend.Contexts;
using CryptoTracker_backend.Middlewares;
using CryptoTracker_backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));

builder.Services.AddHostedService<AlertWorker>()
    .AddSingleton<IPriceChecker, PriceChecker>()
    .AddSingleton<INotificationService, EmailNotificationSevice>();
    
   builder.Services.AddScoped<IAlertService, AlertService>();
   builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    }); 
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddHttpClient("CryptoAPI",a=> {
    a.BaseAddress = new Uri("https://api.coingecko.com");
 });

builder.Services.AddCors(options =>
{
    options.AddPolicy(name:"CryptoCors", policy =>
    {
        policy.WithOrigins("http://localhost:3000").AllowAnyHeader()
                                                   .AllowAnyMethod(); 
    });
});

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CryptoCors");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();
