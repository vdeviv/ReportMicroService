using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Report.Application.Services;
using Report.Domain.Interfaces;

namespace Report.Api.Controllers;

[Authorize] // Protege el endpoint
[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly ReporteAppService _service;
    private readonly IPdfService _pdfService;

    public ReportController(ReporteAppService service, IPdfService pdfService)
    {
        _service = service;
        _pdfService = pdfService;
    }

    [HttpGet("venta/{saleId}/pdf")]
    public async Task<IActionResult> GetReporte(string saleId, string clientId, int userId)
    {
        // Sin pasar el token, el sistema lo inyectará solo en la capa de infraestructura
        var reporteData = await _service.GenerarReporteCompleto(saleId, clientId, userId);

        var pdfBytes = _pdfService.GenerarVentaPdf(reporteData);
        return File(pdfBytes, "application/pdf", $"Comprobante_{saleId}.pdf");
    }
}