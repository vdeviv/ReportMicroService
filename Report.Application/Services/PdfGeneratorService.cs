using System;
using System.Collections.Generic;
using System.Text;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Report.Domain.Models;

namespace Report.Application.Services;

public class PdfGeneratorService
{
    public byte[] GenerarPdfVenta(VentaReporte data)
    {
        var documento = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(1, Unit.Centimetre);

                // CABECERA (Anexo 2)
                page.Header().Row(row => {
                    row.RelativeItem().Column(col => {
                        col.Item().Text("COMPROBANTE DE VENTA").FontSize(20).SemiBold();
                        col.Item().Text($"Fecha: {data.Fecha:dd/MM/yyyy}");
                        col.Item().Text($"CI/NIT: {data.Nit}   Razón Social: {data.RazonSocial}");
                    });
                });

                // TABLA DE PRODUCTOS
                page.Content().Table(table => {
                    table.ColumnsDefinition(columns => {
                        columns.ConstantColumn(50); // Cantidad
                        columns.RelativeColumn();   // Descripción
                        columns.ConstantColumn(80); // Precio Unitario
                        columns.ConstantColumn(80); // Importe
                    });

                    table.Header(header => {
                        header.Cell().Text("Cantidad");
                        header.Cell().Text("Descripción");
                        header.Cell().Text("Precio Unitario Bs.");
                        header.Cell().Text("Importe Bs.");
                    });

                    foreach (var item in data.Items)
                    {
                        table.Cell().Text(item.Cantidad.ToString());
                        table.Cell().Text(item.Descripcion);
                        table.Cell().Text(item.PrecioUnitario.ToString("F2"));
                        table.Cell().Text(item.Importe.ToString("F2"));
                    }
                });

                // PIE DE PÁGINA
                page.Footer().Column(col => {
                    col.Item().AlignRight().Text($"Total Bs: {data.Total:F2}").SemiBold();
                    col.Item().AlignRight().Text($"{DateTime.Now} - {data.UsuarioGenerador}").FontSize(9);
                });
            });
        });

        return documento.GeneratePdf();
    }
}