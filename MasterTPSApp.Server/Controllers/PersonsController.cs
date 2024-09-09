using Microsoft.AspNetCore.Mvc;

public class PersonDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
    public string PersonalIdNumber { get; set; }
    public long Height { get; set; }
    public long PlaceOfBirthId { get; set; }
    public long? PlaceOfResidenceId { get; set; }
}



[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonsController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPersons()
    {
        var persons = await _personService.GetAllPersonsAsync();
        return Ok(persons);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson(long id)
    {
        var person = await _personService.GetPersonByIdAsync(id);
        if (person == null)
        {
            return NotFound();
        }
        return Ok(person);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] PersonDto personDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var person = new Person
        {
            Name = personDto.Name,
            Surname = personDto.Surname,
            BirthDate = personDto.BirthDate,
            PersonalIdNumber = personDto.PersonalIdNumber,
            Height = personDto.Height,
            PlaceOfBirthId = personDto.PlaceOfBirthId,
            PlaceOfResidenceId = personDto.PlaceOfResidenceId
        };
        await _personService.AddPersonAsync(person);
        return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(long id, [FromBody] PersonDto personDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingPerson = await _personService.GetPersonByIdAsync(id);
        if (existingPerson == null)
        {
            return NotFound();
        }

        existingPerson.Name = personDto.Name;
        existingPerson.Surname = personDto.Surname;
        existingPerson.BirthDate = personDto.BirthDate;
        existingPerson.PersonalIdNumber = personDto.PersonalIdNumber;
        existingPerson.Height = personDto.Height;
        existingPerson.PlaceOfBirthId = personDto.PlaceOfBirthId;
        existingPerson.PlaceOfResidenceId = personDto.PlaceOfResidenceId;

        await _personService.UpdatePersonAsync(existingPerson);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(long id)
    {
        var person = await _personService.GetPersonByIdAsync(id);
        if (person == null)
        {
            return NotFound();
        }
        await _personService.DeletePersonAsync(id);
        return NoContent();
    }
}
