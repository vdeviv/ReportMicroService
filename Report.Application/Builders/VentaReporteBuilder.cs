using System;
using System.Collections.Generic;
using System.Text;

using Report.Domain.Interfaces;
using Report.Domain.Models;

namespace Report.Application.Builders;

public class VentaReporteBuilder : IVentaReporteBuilder
{
    private VentaReporte _reporte = new();

    public void Reset() => _reporte = new VentaReporte();

    public void SetEncabezado(DateTime fecha, string nit, string cliente)
    {
        _reporte.Fecha = fecha;
        _reporte.ClientNit = nit;
        _reporte.RazonSocial = cliente;
    }

    public void AgregarProductos(List<DetalleLinea> items)
    {
        _reporte.Items = items;
    }

    public void SetPiePagina(string usuario, decimal total)
    {
        _reporte.UsuarioGenerador = usuario;
        _reporte.Total = total;
        _reporte.TotalEnLetras = $"Son {total:N2} Bolivianos"; // Aquí podrías usar una librería para convertir números a letras
    }

    public VentaReporte GetReporte() => _reporte;
}