using Location.Data.Repository.Cities;
using Location.Data.Repository.Geopoints;
using Location.Data.Repository.Towns;
using Location.Logic.Logics.Cities;
using Location.Logic.Logics.Geopoints;
using Location.Logic.Logics.Towns;
using LocationnWebAPI.Services.Cipher;
using LocationnWebAPI.Services.Jwt;
using LocationnWebAPI.Services.Locations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//Mapper Service
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//JWT
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddAuthentication(option => option.DefaultAuthenticateScheme = "MyJwtProvider")
          .AddJwtBearer("MyJwtProvider", options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value ?? throw new ArgumentNullException())),
                  ValidateIssuer = false,
                  ValidateAudience = false
              };
          });
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme(\"bearer{token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});
//Services dependencies
builder.Services.AddScoped<ICipherService, CipherService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ITownRepository, TownRepository>();
builder.Services.AddScoped<ITownLogic, TownLogic>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityLogic, CityLogic>();
builder.Services.AddScoped<IGeopointRepository, GeopointRepository>();
builder.Services.AddScoped<IGeopointLogic, GeopointLogic>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseApiVersioning();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.UseCors();
app.Run();
