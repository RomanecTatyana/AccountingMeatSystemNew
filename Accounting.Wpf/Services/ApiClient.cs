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

        public async Task<(bool IsSuccess, string ErrorMessage, Item? CreatedItem)> CreateItemAsync(CreateItemRequest request)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("/api/items", request);

            if (!response.IsSuccessStatusCode)
            {
                string errorText = await response.Content.ReadAsStringAsync();

                return (
                    false,
                    string.IsNullOrWhiteSpace(errorText)
                        ? $"Помилка API: {response.StatusCode}"
                        : errorText,
                    null
                );
            }

            Item? createdItem = await response.Content.ReadFromJsonAsync<Item>();

            return (true, "", createdItem);
        }
    }
    public class HealthResponse
    {
        public string Status { get; set; } = "";
        public string Service { get; set; } = "";
        public string Message { get; set; } = "";
    }

    public class CreateItemRequest
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Article { get; set; } = "";
        public string Barcode { get; set; } = "";
        public string Unit { get; set; } = "";
        public string GroupName { get; set; } = "";
        public string ItemType { get; set; } = "";
        public string Comment { get; set; } = "";
    }
}
