using Location.Data.Models;

namespace LocationnWebAPI.Services.Locations
{
    public interface ILocationService
    {
        public int? GetCityIfNotExistCreateAndGet(string name);
        public int? GetTownIfNotExistCreateAndGet(string name);
    }
}
