using System;
using System.Collections.Generic;
using System.Text;

using Report.Domain.Interfaces;
using Report.Domain.Models;
using Report.Infrastructure.Data;

namespace Report.Application.Services;

public class ReporteService
{
    private readonly IVentaReporteBuilder _builder;
    private readonly FarmaApiClient _apiClient;

    public ReporteService(IVentaReporteBuilder builder, FarmaApiClient apiClient)
    {
        _builder = builder;
        _apiClient = apiClient;
    }

    public async Task<VentaReporte> GenerarComprobanteAsync(string saleId, string clientId, int userId)
    {
        // Llamadas asíncronas a los otros microservicios
        var clientData = await _apiClient.GetClient(clientId);
        var saleItems = await _apiClient.GetSaleItems(saleId);
        var userData = await _apiClient.GetUser(userId);

        _builder.Reset();

        // Aplicando el patrón Builder con los datos obtenidos
        if (clientData != null)
            _builder.SetEncabezado(DateTime.Now, clientData.nit, $"{clientData.first_name} {clientData.last_name}");

        if (saleItems != null)
        {
            var detalles = saleItems.Select(x => new DetalleLinea
            {
                Cantidad = x.Quantity,
                Descripcion = x.MedName,
                PrecioUnitario = x.Price
            }).ToList();

            _builder.AgregarProductos(detalles);
            _builder.SetPiePagina(userData?.username ?? "Cajero", detalles.Sum(d => d.Importe));
        }

        return _builder.GetReporte();
    }
}