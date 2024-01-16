using BloodBank.Data.Repository.Hospitals;
using BloodBank.Logic.Logics.Hospitals;
using Donor.Data.Repository.Branches;
using Donor.Data.Repository.DonationHistories;
using Donor.Data.Repository.Donors;
using Donor.Logic.Logics.Branches;
using Donor.Logic.Logics.DonationHistories;
using Donor.Logic.Logics.Donors;
using Donor.Logic.Logics.JoinTable;
using DonorWebAPI.Services.Cipher;
using DonorWebAPI.Services.Donors;
using DonorWebAPI.Services.Jwt;
using DonorWebAPI.Services.Location;
using DonorWebAPI.Services.Security;
using Location.Data.Repository.Cities;
using Location.Data.Repository.Towns;
using Location.Logic.Logics.Cities;
using Location.Logic.Logics.Towns;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Donor.Logic.Logics.Branhces;
using User.Data.Repository.Users;
using User.Logic.Logics.Users;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IJwtService, JwtService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
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
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ICipherService, CipherService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<IJoinTable, JoinTable>();
//User

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserLogic, UserLogic>();

builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
builder.Services.AddScoped<IHospitalLogic, HospitalLogic>();
builder.Services.AddScoped<ITownRepository, TownRepository>();
builder.Services.AddScoped<ITownLogic, TownLogic>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityLogic, CityLogic>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IBranchLogic, BranchLogic>();
builder.Services.AddScoped<IDonorRepository, DonorRepository>();
builder.Services.AddScoped<IDonorLogic, DonorLogic>();
builder.Services.AddScoped<IDonationHistoryRepository, DonationHistoryRepository>();
builder.Services.AddScoped<IDonationHistoryLogic, DonationHistoryLogic>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseApiVersioning();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

//First Authentication then Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors();

app.Run();
