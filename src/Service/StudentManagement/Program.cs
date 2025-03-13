using Microsoft.EntityFrameworkCore;
using StudentManagement.Models.Models;
using StudentManagement.Repository;
using StudentManagement.Repository.Interfaces;
using StudentManagement.Domain.Validators;
using FluentValidation;
using StudentManagement.API.Configuration;
using Microsoft.OpenApi.Models;

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

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StudentManagementContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentManagement.API", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "StudentManagement.API", Version = "v2" });
});

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new AcceptHeaderReader(new AppConfiguration(builder.Configuration));
    config.ErrorResponses = new ApiVersioningErrorResponseProvider();
});


builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentManagement.API v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "StudentManagement.API v2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
