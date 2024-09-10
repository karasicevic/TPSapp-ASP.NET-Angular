using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PlaceService : IPlaceService
{
    private readonly AppDbContext _context;

    public PlaceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Place>> GetAllPlacesAsync()
    {
        return await _context.Places.ToListAsync();
    }

    public async Task<Place> GetPlaceByIdAsync(long id)
    {
        return await _context.Places.FindAsync(id);
    }

    public async Task AddPlaceAsync(Place place)
    {
        _context.Places.Add(place);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePlaceAsync(Place place)
    {
            var existingPlace = await _context.Places.FindAsync(place.Id);
            if (existingPlace == null)
            {
                throw new Exception("Place not found");
            }

            existingPlace.Name = place.Name;
            existingPlace.PostalCode = place.PostalCode;
            existingPlace.Population = place.Population;

            await _context.SaveChangesAsync();
        
    }

    public async Task DeletePlaceAsync(long id)
    {
        var place = await _context.Places.FindAsync(id);
        if (place != null)
        {
            _context.Places.Remove(place);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<float?> GetAverageHeightAsync(long placeId)
    {
        bool anyPerson = await _context.Persons
                                   .AnyAsync(p => p.PlaceOfResidenceId == placeId);

        if (!anyPerson)
        {
            return 0;
        }



        var sql = "SELECT dbo.AverageHeight(@PlaceId)";
        var parameter = new SqlParameter("@PlaceId", placeId);

        var resultList = await _context.Database
            .SqlQueryRaw<double>(sql, parameter).ToListAsync();
        var result = resultList
            .FirstOrDefault();
        

        return (float)result;
    }

    public async Task<long> GetMinHeightAsync(long placeId)
    {
        bool anyPerson = await _context.Persons
                                   .AnyAsync(p => p.PlaceOfResidenceId == placeId);

        if (!anyPerson)
        {
            return 0; 
        }
        var sql = "SELECT dbo.MinHeight(@PlaceId)";
        var parameter = new SqlParameter("@PlaceId", placeId);

        var resultList = await _context.Database
            .SqlQueryRaw<long>(sql, parameter).ToListAsync();
        var result = resultList
            .FirstOrDefault();


        return result;
    }

    public async Task<long> GetMaxHeightAsync(long placeId)
    {
        bool anyPerson = await _context.Persons
                                   .AnyAsync(p => p.PlaceOfResidenceId == placeId);

        if (!anyPerson)
        {
            return 0;
        }
        var sql = "SELECT dbo.MaxHeight(@PlaceId)";
        var parameter = new SqlParameter("@PlaceId", placeId);

        var resultList = await _context.Database
            .SqlQueryRaw<long>(sql, parameter).ToListAsync();
        var result = resultList
            .FirstOrDefault();

        return result;
    }

    public async Task<float> GetAverageAgeAsync(long placeId)
    {
        bool anyPerson = await _context.Persons
                                   .AnyAsync(p => p.PlaceOfResidenceId == placeId);

        if (!anyPerson)
        {
            return 0;
        }
        var sql = "SELECT dbo.averageAge(@PlaceId)";
        var parameter = new SqlParameter("@PlaceId", placeId);

        var resultList = await _context.Database
            .SqlQueryRaw<double>(sql, parameter).ToListAsync();
        var result = resultList
            .FirstOrDefault();


        return (float)result;
    }
}
