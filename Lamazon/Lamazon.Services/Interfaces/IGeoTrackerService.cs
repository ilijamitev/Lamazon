using Lamazon.Services.Models;
using System.Threading.Tasks;

namespace Lamazon.Services.Interfaces
{
    public interface IGeoTrackerService
    {
        Task<IpGeoInfo> GetIpGeoInfo(string ipAddress);
        Task<string> GetCountryFlag(string countryCode);
    }
}
