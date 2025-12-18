using System;
using System.Collections.Generic;
using System.Text;

using Report.Domain.Models;

namespace Report.Domain.Interfaces;

public interface IFarmaApiClient
{
    // Quitar el parámetro token de aquí también
    Task<dynamic?> GetClientAsync(string id);
    Task<List<dynamic>?> GetSaleItemsAsync(string saleId);
    Task<dynamic?> GetUserAsync(int id);
}