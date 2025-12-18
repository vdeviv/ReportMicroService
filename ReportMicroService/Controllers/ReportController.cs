using Microsoft.AspNetCore.Mvc;
using Report.Application.Services;
using Report.Domain.Models;

namespace Report.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly ReporteService _reporteService;

    public ReportController(ReporteService reporteService)
    {
        _reporteService = reporteService;
    }

    [HttpPost("generar")]
    public ActionResult<VentaReporte> GenerarReporte([FromBody] ReportRequest request)
    {
        // En un escenario real, aquí llamarías a tus Gateways en Infrastructure
        // para traer los datos de Client.Api, Sale.Api y User.Api usando los IDs

        var reporte = _reporteService.CrearComprobanteVenta(
            request.Venta,
            request.Cliente,
            request.Usuario,
            request.Items
        );

        return Ok(reporte);
    }
}

public class ReportRequest
{
    public dynamic Venta { get; set; }
    public dynamic Cliente { get; set; }
    public dynamic Usuario { get; set; }
    public List<dynamic> Items { get; set; }
}