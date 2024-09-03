using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using NoteApp;
using NoteApp.Abstractions;
using NoteApp.Configurations.Mappings;
using NoteApp.Services.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuth(builder.Configuration);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplicationServices()
    .AddControllers();

builder.Services.AddAutoMapper((serviceProvider, configuration) =>
{
    var timeProvider = serviceProvider.GetRequiredService<ITimeProvider>();
    configuration.AddProfile(new NoteMappingProfile(timeProvider));
}, Assembly.GetExecutingAssembly());

builder.Services.Configure<JsonOptions>(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


var app = builder.Build();

app.UseExceptionHandler("/error");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(); 
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
