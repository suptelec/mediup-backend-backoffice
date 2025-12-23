using MediUp.Application.Services.ElectriCompanies;
using MediUp.Domain.Interfaces.Repositories;
using MediUp.Domain.Interfaces.Services;
using MediUp.Infrastructure.Persistence;
using MediUp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("MediUpBackofficeDb");
});
builder.Services.AddScoped<IElectriCompanyRepository, ElectriCompanyRepository>();
builder.Services.AddScoped<IAppDataService, AppDataService>();
builder.Services.AddScoped<IElectriCompanyService, ElectriCompanyService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "MediUp Backoffice API V1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
