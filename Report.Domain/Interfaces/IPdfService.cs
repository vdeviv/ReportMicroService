using System;
using System.Collections.Generic;
using System.Text;

using Report.Domain.Models;

namespace Report.Domain.Interfaces;

public interface IPdfService
{
    byte[] GenerarVentaPdf(VentaReporte data);
}