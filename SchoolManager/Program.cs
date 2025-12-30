using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Data;
using SchoolManager.Data.Repositories;
using SchoolManager.Data.Repositories.Interfaces;
using SchoolManager.Services;
using SchoolManager.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentServices, StudentServices>();

builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ITeacherServices, TeacherServices>();

builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ISubjectServices, SubjectServices>();

builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IClassServices,ClassServices>();

builder.Services.AddScoped<IStudentSubjectRepository, StudentSubjectRepository>();
builder.Services.AddScoped<IStudentSubjectServices, StudentSubjectServices>();

builder.Services.AddScoped<ISubjectTeacherRepository, SubjectTeacherRepository>();
builder.Services.AddScoped<ISubjectTeacherServices, SubjectTeacherServices>();


builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
