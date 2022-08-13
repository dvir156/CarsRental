using AutoMapper;
using CarsCatalog.Api.Base;
using CarsCatalog.Api.Managers;
using CarsCatalog.Api.Models;
using CarsCatalog.Managers;
using CarsCatalog.Models;
using CarsCatalog.Repositories;
using CarsCatalog.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:6055",
            ValidAudience = "https://localhost:6055",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@NetBet"))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var config = builder.Configuration.GetSection("Settings").Get<Settings>();
builder.Services.AddSingleton(config);
var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<CarDto, Car>());
builder.Services.AddSingleton(mapperConfig.CreateMapper());
builder.Services.AddSingleton<ICarsCatalogRepository, CarsCatalogRepository>();
builder.Services.AddSingleton<IDriverAgeValidator, DriverAgeValidator>();
builder.Services.AddSingleton<IRentDatesValidator, RentDatesValidator>();
builder.Services.AddSingleton<ICarsCatalogManager, CarsCatalogManager>();
builder.Services.AddSingleton<IAuthManager, AuthManager>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("EnableCORS");
app.MapControllers();

app.Run();
