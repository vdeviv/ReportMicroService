using System;
using System.Collections.Generic;
using System.Text;

using Report.Domain.Interfaces;
using Report.Domain.Models;

namespace Report.Application.Builders;

public class VentaReporteBuilder
{
    private VentaReporte _reporte = new();

    public void Reset() => _reporte = new VentaReporte();

    public void SetDatosCliente(string nombre, string nit)
    {
        _reporte.RazonSocial = nombre;
        _reporte.Nit = nit;
        _reporte.Fecha = DateTime.Now;
    }

    public void SetProductos(List<DetalleLinea> items)
    {
        _reporte.Items = items;
        _reporte.Total = items.Sum(i => i.Importe);
    }

    public void SetUsuario(string username) => _reporte.UsuarioGenerador = username;

    public VentaReporte Build() => _reporte;
}