using Report.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Domain.Interfaces;

public interface IVentaReporteBuilder
{
    void Reset();
    void SetEncabezado(DateTime fecha, string nit, string cliente);
    void AgregarProductos(List<DetalleLinea> items);
    void SetPiePagina(string usuario, decimal total);
    VentaReporte GetReporte();
}