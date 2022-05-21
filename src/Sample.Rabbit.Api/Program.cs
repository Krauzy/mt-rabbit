global using Microsoft.AspNetCore.Mvc;
global using Microsoft.OpenApi.Models;
global using System.Security.Claims;

using Sample.Rabbit.Core;
using Sample.Rabbit.Core.Extensions;
using Serilog;

SerilogExtensions.AddSerilog("API Sample");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers();
builder.Services.UseOpenTelemetry(appSettings);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Sample.Rabbit.Api", Version = "v1" });
});

builder.Services.UseMassTransit(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample.Rabbit.Api v1"));
}

app.MapControllers();

await app.RunAsync();