using Microsoft.AspNetCore.Http; // Necesario para IHttpContextAccessor
using Report.Domain.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Report.Infrastructure.Data;

public class FarmaApiClient : IFarmaApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FarmaApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpClient CreateClient(string clientName)
    {
        var client = _httpClientFactory.CreateClient(clientName);

        // PROPAGACIÓN DE TOKEN (Igual a tu ejemplo)
        var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"]
                    .ToString().Replace("Bearer ", "");

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return client;
    }

    public async Task<dynamic?> GetClientAsync(string id)
        => await CreateClient("ClientsApi").GetFromJsonAsync<dynamic>($"api/clients/{id}");

    public async Task<List<dynamic>?> GetSaleItemsAsync(string saleId)
        => await CreateClient("SalesApi").GetFromJsonAsync<List<dynamic>>($"api/sales/{saleId}/items");

    public async Task<dynamic?> GetUserAsync(int id)
        => await CreateClient("UsersApi").GetFromJsonAsync<dynamic>($"api/users/{id}");
}