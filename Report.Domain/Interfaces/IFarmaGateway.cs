using System.Collections.Generic;
using System.Threading.Tasks;

namespace Report.Domain.Interfaces
{
    public interface IFarmaGateway
    {
        // Recupera los datos básicos del cliente (Nombre, NIT, etc.)
        Task<dynamic?> GetClientAsync(string id);

        // Recupera la lista de productos/detalles de una venta específica
        Task<List<dynamic>?> GetSaleItemsAsync(string saleId);

        // Recupera los datos del usuario o cajero que realizó la operación
        Task<dynamic?> GetUserAsync(int id);
    }
}