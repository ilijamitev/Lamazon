using Lamazon.Services;
using Lamazon.Services.Interfaces;
using Lamazon.Services.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lamazon.Services.Implementations
{
    public class GeoTrackerService : BaseService, IGeoTrackerService
    {
        private readonly HttpClient _httpClient;
        public GeoTrackerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetCountryFlag(string countryCode)
        {
            return $"https://flagcdn.com/w320/{countryCode.ToLower()}.png";
        }

        public async Task<IpGeoInfo> GetIpGeoInfo(string ipAddress)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"http://ip-api.com/json/{ipAddress}");
            var jsonData = await httpResponseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IpGeoInfo>(jsonData);
        }
    }
}
