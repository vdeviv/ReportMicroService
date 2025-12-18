using Report.Domain.Interfaces;
using Report.Domain.Models;
using Report.Application.Builders;

namespace Report.Application.Services;

public class ReporteAppService
{
    private readonly IFarmaGateway _gateway; // Cambio de nombre a Gateway
    private readonly VentaReporteBuilder _builder;

    public ReporteAppService(IFarmaGateway gateway, VentaReporteBuilder builder)
    {
        _gateway = gateway;
        _builder = builder;
    }

    public async Task<VentaReporte> GenerarReporteCompleto(string saleId, string clientId, int userId)
    {
        // Llamadas limpias: el token se propaga automáticamente en la capa de infraestructura
        var cliente = await _gateway.GetClientAsync(clientId);
        var items = await _gateway.GetSaleItemsAsync(saleId);
        var usuario = await _gateway.GetUserAsync(userId);

        _builder.Reset();

        // Mapeo de datos del cliente
        _builder.SetDatosCliente($"{cliente?.first_name} {cliente?.last_name}", cliente?.nit?.ToString() ?? "S/N");

        // Mapeo de ítems de la venta
        // Mapeo de ítems de la venta corregido
        var detalles = items?.Select(i => new DetalleLinea
        {
            Cantidad = i.quantity,
            Descripcion = i.medicine_name ?? "Producto",
            PrecioUnitario = i.unit_price
            // IMPORTANTE: Eliminamos la línea "Importe = ...", ya que es de solo lectura
        }).ToList() ?? new List<DetalleLinea>();

        _builder.SetProductos(detalles);
        _builder.SetUsuario(usuario?.username?.ToString() ?? "Cajero");

        return _builder.Build();
    }
}