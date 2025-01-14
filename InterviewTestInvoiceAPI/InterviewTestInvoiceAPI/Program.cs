using InterviewTestInvoiceAPI;
using InterviewTestInvoiceAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string secretKey = builder.Configuration["Authentication:SecretKey"]?.ToString() ?? "";
var key = Encoding.UTF8.GetBytes(secretKey);
string issuerName = builder.Configuration["Authentication:Issuer"]?.ToString() ?? "";
string audienceName = builder.Configuration["Authentication:Audience"]?.ToString() ?? "";

builder.Services.AddAuthentication(options =>
                                    {
                                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                    })
               .AddJwtBearer(options =>
                                    {
                                        options.TokenValidationParameters = new TokenValidationParameters
                                        {
                                            ValidateIssuer = true, // Set true if you want to validate the issuer
                                            ValidateAudience = true, // Set true if you want to validate the audience
                                            ValidateIssuerSigningKey = true,
                                            IssuerSigningKey = new SymmetricSecurityKey(key),
                                            ValidateLifetime = true,
                                            ValidIssuer = issuerName,
                                            ValidAudience = audienceName
                                        };
                                    });

builder.Services.AddAuthorization();


builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("FixedPolicy", limiterOptions =>
    {
        limiterOptions.PermitLimit = 100;
        limiterOptions.Window = TimeSpan.FromSeconds(10);
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 10;
    });
});

var connectionString = builder.Configuration.GetConnectionString("TestInvoice");

builder.Services.AddDbContext<TestDbContext>(options => options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));
builder.Services.AddScoped<ITestInvoice, TestInvoiceRepository>();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console() // Log to console
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Replace default logging with Serilog
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
