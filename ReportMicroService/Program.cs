using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Report.Application.Builders;
using Report.Application.Services;
using Report.Domain.Interfaces;
using Report.Infrastructure.Data;
using Report.Infrastructure.Gateways;
using Report.Infrastructure.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

/// Configuración de JWT (Basado en tu ejemplo)
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection["Key"] ?? throw new InvalidOperationException("Jwt:Key no configurado");
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ClockSkew = TimeSpan.Zero
        };
    });

// INFRAESTRUCTURA NECESARIA
builder.Services.AddHttpContextAccessor(); // Para capturar el token del usuario actual
builder.Services.AddHttpClient();           // Para usar IHttpClientFactory

// Registro de los microservicios externos como Clientes con nombre
builder.Services.AddHttpClient("ClientsApi", c => c.BaseAddress = new Uri("http://localhost:5142/"));
builder.Services.AddHttpClient("SalesApi", c => c.BaseAddress = new Uri("http://localhost:5104/"));
builder.Services.AddHttpClient("UsersApi", c => c.BaseAddress = new Uri("http://localhost:5031/"));


builder.Services.AddScoped<IPdfService, QuestPdfService>();
builder.Services.AddScoped<VentaReporteBuilder>();
builder.Services.AddScoped<ReporteAppService>();

builder.Services.AddScoped<IFarmaGateway, FarmaGateway>();

builder.Services.AddControllers();
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();