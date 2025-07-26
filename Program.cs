using System.Diagnostics.Metrics;
using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TourTravel.Models;
using TourTravel.Validators;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register all validators from this assembly
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
//builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();    
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);


builder.Services.AddDbContext<TourManagementContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(name: "myConnectionStrings")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
