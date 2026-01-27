using AspNetCore.Swagger.Themes;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Extensions;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options => options.UseInlineDefinitionsForEnums());
builder.Services.AddDb(builder);
builder.Services.AddServices();
builder.Services.AddClient();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(Theme.Dark, c => c.EnableThemeSwitcher());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
