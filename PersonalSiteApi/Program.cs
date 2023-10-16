using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PersonalSiteApi;
using PersonalSiteApi.EntityFramework;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PersonalSiteContext>();
var test = builder.Configuration.GetValue<string>("JwtSettings:Issuer");

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueCountLimit = 1024; //default 1024
    options.ValueLengthLimit = int.MaxValue; //not recommended value
    options.MultipartBodyLengthLimit = long.MaxValue; //not recommended value
    options.MemoryBufferThreshold = Int32.MaxValue;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:Key")))
            };
        });

builder.Services.AddCors(cors =>
{
    cors.AddPolicy("Allow all", policy =>
    {
        policy.WithOrigins("*");
        policy.WithMethods("*");
        policy.WithHeaders("*");
    });
});

var app = builder.Build();

app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Allow all");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
