using Bellosoft.Interfaces;
using Bellosoft.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace Bellosoft.Service
{
    public class NasaApodService : INasaApod
    {
        private readonly HttpClient _http;
        private readonly Context _db;
        private readonly IConfiguration _config;

        public NasaApodService(HttpClient httpClient, Context db, IConfiguration config)
        {
            _http = httpClient;
            _db = db;
            _config = config;
        }

        public async Task<NasaApod?> GetTodayAsync(CancellationToken ct = default)
        {
            var apiKey = _config["Nasa:ApiKey"];
            var baseUrl = _config["Nasa:BaseUrl"];
            var today = DateTime.UtcNow.Date;

            var url = $"{baseUrl}?api_key={apiKey}&date={today:yyyy-MM-dd}";
           
            return await _http.GetFromJsonAsync<NasaApod>(url, cancellationToken: ct);
        }

    }
}

