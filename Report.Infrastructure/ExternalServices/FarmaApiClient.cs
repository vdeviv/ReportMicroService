using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Http.Json;
using Report.Infrastructure.External;

namespace Report.Infrastructure.Data;

public class FarmaApiClient
{
    private readonly HttpClient _http;

    public FarmaApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<ExternalClientResponse?> GetClient(string id) =>
        await _http.GetFromJsonAsync<ExternalClientResponse>($"http://tu-api-clients/api/clients/{id}");

    public async Task<List<ExternalSaleItemResponse>?> GetSaleItems(string saleId) =>
        await _http.GetFromJsonAsync<List<ExternalSaleItemResponse>>($"http://tu-api-sales/api/sales/{saleId}/items");

    public async Task<ExternalUserResponse?> GetUser(int id) =>
        await _http.GetFromJsonAsync<ExternalUserResponse>($"http://tu-api-users/api/users/{id}");
}