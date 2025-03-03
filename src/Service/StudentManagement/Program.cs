using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement.Domain.Handlers;
using StudentManagement.Models.Models;
using StudentManagement.Repository;
using StudentManagement.Repository.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using MediatR;
using StudentManagement.Domain.Validators;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(StudentManagement.Domain.Handlers.GetStudentsQueryHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(StudentManagement.Domain.Handlers.GetStudentsByIdQueryHandler).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(GetStudentsByIdQueryValidator).Assembly);

builder.Services.AddTransient<IStudentRepository, StudentRepository>();

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StudentManagementContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "StudentManagement.API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c=> c.SwaggerEndpoint("/swagger/v1/swagger.json","StudentManagement.API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
