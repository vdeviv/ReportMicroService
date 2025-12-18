using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Report.Domain.Interfaces;

namespace Report.Infrastructure.Gateways;

public class FarmaGateway : IFarmaGateway
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FarmaGateway(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpClient CreateClient(string clientName)
    {
        var client = _httpClientFactory.CreateClient(clientName);

        // Propagación de Token (Idéntico a tu LotGateway.cs)
        var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"]
                    .ToString().Replace("Bearer ", "");

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return client;
    }

    public async Task<dynamic?> GetClientAsync(string id)
    {
        var client = CreateClient("ClientsApi"); // Debe llamarse igual que en Program.cs
        return await client.GetFromJsonAsync<dynamic>($"api/clients/{id}");
    }

    public async Task<List<dynamic>?> GetSaleItemsAsync(string saleId)
    {
        var client = CreateClient("SalesApi"); // Debe llamarse igual que en Program.cs
        return await client.GetFromJsonAsync<List<dynamic>>($"api/sales/{saleId}/items");
    }

    public async Task<dynamic?> GetUserAsync(int id)
    {
        var client = CreateClient("UsersApi"); // Debe llamarse igual que en Program.cs
        return await client.GetFromJsonAsync<dynamic>($"api/users/{id}");
    }
}