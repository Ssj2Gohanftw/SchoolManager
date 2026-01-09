using AspNetCore.Swagger.Themes;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using SchoolManager;
using SchoolManager.Data;
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
builder.Services.AddSwaggerGen(options=> options.UseInlineDefinitionsForEnums());
var connString = builder.Configuration.GetConnectionString("DefaultConnection");
var dataSource = new NpgsqlDataSourceBuilder(connString).EnableDynamicJson().Build();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(dataSource)
);

builder.Services.AddServices();
builder.Services.AddHttpClient("University", client =>
{
    client.BaseAddress = new Uri("http://universities.hipolabs.com/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("User-Agent", "SchoolManager/1.0");
});
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(Theme.Dark,c=>c.EnableThemeSwitcher());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
