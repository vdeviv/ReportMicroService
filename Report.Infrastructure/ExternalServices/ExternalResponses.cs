using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Infrastructure.External;

// Lo que esperamos de ClientsMicroservice
public class ExternalClientResponse
{
    public string first_name { get; set; } = string.Empty;
    public string last_name { get; set; } = string.Empty;
    public string nit { get; set; } = string.Empty;
}

// Lo que esperamos de SaleDetailMicroservice
public class ExternalSaleItemResponse
{
    public string MedName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

// Lo que esperamos de User.Api
public class ExternalUserResponse
{
    public string username { get; set; } = string.Empty;
}