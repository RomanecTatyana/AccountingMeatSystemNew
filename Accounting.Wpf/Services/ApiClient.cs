using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using Accounting.Domain.Entities;

namespace Accounting.Wpf.Services
{
    public class ApiClient
    {
        private readonly HttpClient httpClient;

        public ApiClient()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5087")
            };
        }

        public async Task<HealthResponse?> GetHealthAsync()
        {
            return await httpClient.GetFromJsonAsync<HealthResponse>("/api/health");
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            List<Item>? items = await httpClient.GetFromJsonAsync<List<Item>>("/api/items");

            if (items == null)
            {
                return new List<Item>();
            }

            return items;
        }
    }
    public class HealthResponse
    {
        public string Status { get; set; } = "";
        public string Service { get; set; } = "";
        public string Message { get; set; } = "";
    }
}
