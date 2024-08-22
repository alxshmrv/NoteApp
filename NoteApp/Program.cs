using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using NoteApp;
using NoteApp.Abstractions;
using NoteApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSwagger()
    .AddApplicationServices()
    .AddControllers();

builder.Services.Configure<JsonOptions>(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

app.UseExceptionHandler("/error");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(); 
}

app.MapControllers();

app.Run();
