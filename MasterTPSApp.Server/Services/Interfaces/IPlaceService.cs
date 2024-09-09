using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPlaceService
{
    Task<IEnumerable<Place>> GetAllPlacesAsync();
    Task<Place> GetPlaceByIdAsync(long id);
    Task AddPlaceAsync(Place place);
    Task UpdatePlaceAsync(Place place);
    Task DeletePlaceAsync(long id);
    Task<float?> GetAverageHeightAsync(long placeId);
    Task<long> GetMinHeightAsync(long placeId);
    Task<long> GetMaxHeightAsync(long placeId);
    Task<float> GetAverageAgeAsync(long placeId);
}
