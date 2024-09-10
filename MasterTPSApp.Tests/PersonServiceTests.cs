using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MasterTPSApp.Server.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class PersonServiceTests
{
    private readonly DbContextOptions<AppDbContext> _options;

    public PersonServiceTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseInMemoryDatabase(databaseName: "PersonServiceTest")
                        .Options;
    }

    [Fact]
    public async Task GetAllPersonsAsync_Should_Return_All_Persons()
    {
        using (var context = new AppDbContext(_options))
        {
            context.Database.EnsureDeleted();
            context.Persons.AddRange(
                new Person
                {
                    Name = "John",
                    Surname = "Doe",
                    BirthDate = new DateTime(1990, 1, 1),
                    PersonalIdNumber = "1234567890123",
                    Height = 180,
                    PlaceOfBirthId = 1,
                    PlaceOfResidenceId = 2
                },
                new Person
                {
                    Name = "Jane",
                    Surname = "Doe",
                    BirthDate = new DateTime(1994, 2, 2),
                    PersonalIdNumber = "9876543210987",
                    Height = 160,
                    PlaceOfBirthId = 1,
                    PlaceOfResidenceId = 2
                },
                new Person
                {
                    Name = "Jane",
                    Surname = "Doe",
                    BirthDate = new DateTime(1992, 2, 2),
                    PersonalIdNumber = "8526543210987",
                    Height = 170,
                    PlaceOfBirthId = 1,
                    PlaceOfResidenceId = 2
                }
            );

            await context.SaveChangesAsync();

            var service = new PersonService(context);
            var persons = await service.GetAllPersonsAsync();
            Console.Write(persons);

            Assert.Equal(3, persons.Count());
        }
    }

    [Fact]
    public async Task AddPersonAsync_Should_Add_New_Person()
    {
        using (var context = new AppDbContext(_options))
        {
            context.Database.EnsureDeleted();

            var service = new PersonService(context);

            var person = new Person
            {
                Name = "John",
                Surname = "Doe",
                BirthDate = new DateTime(1992, 1, 1),
                PersonalIdNumber = "1234567890123",
                Height = 170,
                PlaceOfBirthId = 1,
                PlaceOfResidenceId = 2
            };

            await service.AddPersonAsync(person);

            Assert.Equal(1, await context.Persons.CountAsync());
        }
    }




    [Fact]
    public async Task GetPersonByIdAsync_Should_Return_Correct_Person()
    {
        using (var context = new AppDbContext(_options))
        {
            context.Database.EnsureDeleted();
            var person = new Person
            {
                Name = "John",
                Surname = "Doe",
                BirthDate = new DateTime(1994, 1, 1),
                PersonalIdNumber = "1234567890123",
                Height = 180,
                PlaceOfBirthId = 1,
                PlaceOfResidenceId = 2
            };

            context.Persons.Add(person);
            await context.SaveChangesAsync();

            var service = new PersonService(context);
            var retrievedPerson = await service.GetPersonByIdAsync(person.Id);

            Assert.NotNull(retrievedPerson);
            Assert.Equal(person.Name, retrievedPerson.Name);
        }
    }

    [Fact]
    public async Task UpdatePersonAsync_Should_Update_Existing_Person()
    {
        using (var context = new AppDbContext(_options))
        {
            context.Database.EnsureDeleted();
            var person = new Person
            {
                Name = "John",
                Surname = "Doe",
                BirthDate = new DateTime(1990, 1, 1),
                PersonalIdNumber = "1234567890123",
                Height = 180,
                PlaceOfBirthId = 1,
                PlaceOfResidenceId = 2
            };

            context.Persons.Add(person);
            await context.SaveChangesAsync();

            var service = new PersonService(context);
            person.Name = "Johnny";
            await service.UpdatePersonAsync(person);

            var updatedPerson = await context.Persons.FindAsync(person.Id);
            Assert.Equal("Johnny", updatedPerson.Name);
        }
    }

    [Fact]
    public async Task DeletePersonAsync_Should_Remove_Person()
    {
        using (var context = new AppDbContext(_options))
        {
            context.Database.EnsureDeleted();
            var person = new Person
            {
                Name = "John",
                Surname = "Doe",
                BirthDate = new DateTime(1990, 1, 1),
                PersonalIdNumber = "1234567890123",
                Height = 180,
                PlaceOfBirthId = 1,
                PlaceOfResidenceId = 2
            };

            context.Persons.Add(person);
            await context.SaveChangesAsync();

            var service = new PersonService(context);
            await service.DeletePersonAsync(person.Id);

            Assert.Equal(0, await context.Persons.CountAsync());
        }
    }

    [Fact]
    public void Person_Should_Have_Valid_Attributes()
    {
        var person = new Person
        {
            Name = "John",
            Surname = "Doe",
            BirthDate = new DateTime(1990, 1, 1),
            PersonalIdNumber = "1234567890123",
            Height = 180,
            PlaceOfBirthId = 1,
            PlaceOfResidenceId = 2
        };

        var validationContext = new ValidationContext(person, null, null);
        var validationResults = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(person, validationContext, validationResults, true);

        Assert.True(isValid);
    }

    //[Fact]
    //public void Person_Should_Fail_Validation_With_Invalid_Attributes()
    //{
    //    var person = new Person
    //    {
    //        Name = "john", // Invalid name (lowercase first letter)
    //        Surname = "doe", // Invalid surname (lowercase first letter)
    //        BirthDate = new DateTime(1940, 1, 1), // Invalid birthdate
    //        PersonalIdNumber = "123456789", // Invalid JMBG (too short)
    //        Height = 180,
    //        PlaceOfBirthId = 1,
    //        PlaceOfResidenceId = 2
    //    };

    //    var validationContext = new ValidationContext(person, null, null);
    //    var validationResults = new List<ValidationResult>();

    //    var isValid = Validator.TryValidateObject(person, validationContext, validationResults, true);

    //    Assert.False(isValid);
    //    Assert.Equal(3, validationResults.Count); // Expecting 3 validation errors
    //}
}
