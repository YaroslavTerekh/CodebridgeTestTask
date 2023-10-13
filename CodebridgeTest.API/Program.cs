using CodebridgeTest.BL.Settings;
using CodebridgeTest.Domain.DbConnection;
using Microsoft.EntityFrameworkCore;
using MediatR;
using FluentValidation;
using CodebridgeTest.BL.Behaviors;
using CodebridgeTest.API.Middleware;
using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextsCustom(builder.Configuration);

builder.Services.AddValidatorsFromAssembly(AppDomain.CurrentDomain.GetAssemblies().Where(t => t.FullName.Contains("BL")).First());
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
var applicationInfo = builder.Configuration
    .GetRequiredSection("Info")
    .Get<ApplicationInfo>();
builder.Services.AddSingleton(applicationInfo);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddOptions();
builder.Services.AddMemoryCache();

builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));

builder.Services.AddInMemoryRateLimiting();

builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();
var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
dataContext.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();
app.UseIpRateLimiting();

app.UseAuthorization();

app.MapControllers();

app.Run();
