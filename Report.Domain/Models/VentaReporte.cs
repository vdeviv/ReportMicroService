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
    public string Logo { get; set; } = "LOGO_BASE64_O_URL";
    public string Titulo { get; set; } = "COMPROBANTE DE VENTA";
    public DateTime Fecha { get; set; }
    public string ClientNit { get; set; } = string.Empty;
    public string RazonSocial { get; set; } = string.Empty;
    public List<DetalleLinea> Items { get; set; } = new();
    public decimal Total { get; set; }
    public string TotalEnLetras { get; set; } = string.Empty;
    public string UsuarioGenerador { get; set; } = string.Empty;
    public string FechaHoraGeneracion => DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss");
}