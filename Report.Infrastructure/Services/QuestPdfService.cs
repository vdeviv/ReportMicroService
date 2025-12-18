using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Report.Domain.Interfaces;
using Report.Domain.Models;
using Report.Domain.Services;

namespace Report.Infrastructure.Services;

public class QuestPdfService : IPdfService
{
    public byte[] GenerarVentaPdf(VentaReporte data)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        // Asignamos el valor en letras antes de generar
        data.TotalEnLetras = NumberToWordsService.Convertir(data.Total);

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Verdana));

                // CABECERA
                page.Header().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        // Dibujo del logo usando bordes simples de QuestPDF
                        col.Item().Width(40).Height(40).Border(1).AlignCenter().AlignMiddle().Text("Logo").FontSize(8);
                    });

                    row.RelativeItem(5).PaddingTop(10).Text("COMPROBANTE DE VENTA")
                        .FontSize(22).ExtraBold().AlignCenter();
                });

                // CONTENIDO
                page.Content().PaddingVertical(10).Column(col =>
                {
                    col.Item().Text($"Fecha: {data.Fecha:dd/MM/yyyy}").SemiBold();
                    col.Item().Row(row =>
                    {
                        row.ConstantItem(150).Text($"CI/NIT: {data.Nit}");
                        row.RelativeItem().Text($"Razón Social: {data.RazonSocial.ToUpper()}");
                    });

                    col.Item().PaddingTop(15).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(80);
                            columns.RelativeColumn();
                            columns.ConstantColumn(100);
                            columns.ConstantColumn(80);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Cantidad");
                            header.Cell().Element(CellStyle).Text("Descripción");
                            header.Cell().Element(CellStyle).Text("Precio Unitario Bs.");
                            header.Cell().Element(CellStyle).Text("Importe Bs.");

                            static IContainer CellStyle(IContainer container) =>
                                container.Border(1).Padding(5).AlignCenter().AlignMiddle();
                        });

                        foreach (var item in data.Items)
                        {
                            table.Cell().Border(1).Padding(5).AlignCenter().Text(item.Cantidad.ToString());
                            table.Cell().Border(1).Padding(5).Text(item.Descripcion);
                            table.Cell().Border(1).Padding(5).AlignRight().Text(item.PrecioUnitario.ToString("N2"));
                            table.Cell().Border(1).Padding(5).AlignRight().Text(item.Importe.ToString("N2"));
                        }
                    });

                    col.Item().AlignRight().PaddingTop(10).Text($"Total Bs: {data.Total:N2}").FontSize(12).Bold();
                    col.Item().PaddingTop(5).Text($"Son {data.TotalEnLetras}");
                });

                page.Footer().AlignRight().Text(x =>
                {
                    x.Span($"{data.Fecha:dd/MM/yyyy} - {data.HoraGeneracion} - {data.UsuarioGenerador}").FontSize(9).Italic();
                });
            });
        }).GeneratePdf();
    }
}