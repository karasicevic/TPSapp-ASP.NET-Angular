using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PlacesController : ControllerBase
{
    private readonly IPlaceService _placeService;
    private readonly IPersonService _personService;

    public PlacesController(IPlaceService placeService, IPersonService personService)
    {
        _placeService = placeService;
        _personService = personService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlaces()
    {
        var places = await _placeService.GetAllPlacesAsync();
        return Ok(places);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlace(long id)
    {
        var place = await _placeService.GetPlaceByIdAsync(id);
        if (place == null)
        {
            return NotFound();
        }
        return Ok(place);
    }

    [HttpPost]
   public async Task<IActionResult> CreatePlace([FromBody] Place place)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    await _placeService.AddPlaceAsync(place);
    return CreatedAtAction(nameof(GetPlace), new { id = place.Id }, place);
}

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlace(long id, [FromBody] Place place)
    {
        if (id!=0)
        {
            place.Id = id;
        }
        else{
            return BadRequest();
        }


        try
        {
            await _placeService.UpdatePlaceAsync(place);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlace(long id)
    {
        var place = await _placeService.GetPlaceByIdAsync(id);
        if (place == null)
        {
            return NotFound();
        }
        await _placeService.DeletePlaceAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/stats")]
    public async Task<IActionResult> GetPlaceStats(long id)
    {
        var place = await _placeService.GetPlaceByIdAsync(id);
        if (place == null)
        {
            Console.WriteLine("nije nasao mesto");
            return NotFound();
        }

        var avgHeight = await _placeService.GetAverageHeightAsync(id);
        var minHeight = await _placeService.GetMinHeightAsync(id);
        var maxHeight = await _placeService.GetMaxHeightAsync(id);
        var avgAge = await _placeService.GetAverageAgeAsync(id);

        return Ok(new
        {
            AverageHeight = avgHeight,
            MinimumHeight = minHeight,
            MaximumHeight = maxHeight,
            AverageAge = avgAge
        });
    }
}
