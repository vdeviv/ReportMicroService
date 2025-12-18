using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Domain.Models;

public class DetalleLinea
{
    public int Cantidad { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal PrecioUnitario { get; set; }
    public decimal Importe => Cantidad * PrecioUnitario;
}

public class VentaReporte
{
    public string Titulo => "COMPROBANTE DE VENTA";
    public DateTime Fecha { get; set; }
    public string RazonSocial { get; set; } = string.Empty;
    public string Nit { get; set; } = string.Empty;
    public List<DetalleLinea> Items { get; set; } = new();
    public decimal Total { get; set; }
    public string TotalEnLetras { get; set; } = string.Empty;
    public string UsuarioGenerador { get; set; } = string.Empty;
    public string HoraGeneracion => DateTime.Now.ToString("HH:mm:ss");
}