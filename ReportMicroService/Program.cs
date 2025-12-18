// Program.cs
using Report.Domain.Interfaces;
using Report.Application.Builders;
using Report.Application.Services;

builder.Services.AddScoped<IVentaReporteBuilder, VentaReporteBuilder>();
builder.Services.AddScoped<ReporteService>();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
