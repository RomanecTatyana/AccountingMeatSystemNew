using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;

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
    }
    public class HealthResponse
    {
        public string Status { get; set; } = "";
        public string Service { get; set; } = "";
        public string Message { get; set; } = "";
    }
}
