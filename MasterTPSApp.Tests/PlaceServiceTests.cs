using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class PlaceServiceTests
{
    private readonly DbContextOptions<AppDbContext> _options;

    public PlaceServiceTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseInMemoryDatabase(databaseName: "PlaceServiceTest")
                        .Options;
    }

    [Fact]
    public async Task GetAllPlacesAsync_Should_Return_All_Places()
    {
        using (var context = new AppDbContext(_options))
        {
            context.Database.EnsureDeleted();

            context.Places.AddRange(
                new Place { Name = "Belgrade", PostalCode = 11000, Population = 1300000 },
                new Place { Name = "Novi Sad", PostalCode = 21000, Population = 400000 }
            );

            await context.SaveChangesAsync();

            var service = new PlaceService(context);
            var places = await service.GetAllPlacesAsync();

            Assert.Equal(2, places.Count());
        }
    }

    [Fact]
    public async Task AddPlaceAsync_Should_Add_New_Place()
    {
        using (var context = new AppDbContext(_options))
        {
            context.Database.EnsureDeleted();

            var service = new PlaceService(context);

            var place = new Place { Name = "Nis", PostalCode = 18000, Population = 250000 };
            await service.AddPlaceAsync(place);

            Assert.Equal(1, await context.Places.CountAsync());
        }
    }

    [Fact]
    public async Task GetPlaceByIdAsync_Should_Return_Correct_Place()
    {
        using (var context = new AppDbContext(_options))
        {
            context.Database.EnsureDeleted();

            var place = new Place { Name = "Subotica", PostalCode = 24000, Population = 100000 };
            context.Places.Add(place);
            await context.SaveChangesAsync();

            var service = new PlaceService(context);
            var retrievedPlace = await service.GetPlaceByIdAsync(place.Id);

            Assert.NotNull(retrievedPlace);
            Assert.Equal(place.Name, retrievedPlace.Name);
        }
    }

    [Fact]
    public async Task UpdatePlaceAsync_Should_Update_Existing_Place()
    {
        using (var context = new AppDbContext(_options))
        {
            context.Database.EnsureDeleted();

            var place = new Place { Name = "Kragujevac", PostalCode = 34000, Population = 150000 };
            context.Places.Add(place);
            await context.SaveChangesAsync();

            var service = new PlaceService(context);
            place.Name = "Kraljevo";
            await service.UpdatePlaceAsync(place);

            var updatedPlace = await context.Places.FindAsync(place.Id);
            Assert.Equal("Kraljevo", updatedPlace.Name);
        }
    }

    [Fact]
    public async Task DeletePlaceAsync_Should_Remove_Place()
    {
        using (var context = new AppDbContext(_options))
        {
            context.Database.EnsureDeleted();

            var place = new Place { Name = "Pancevo", PostalCode = 26000, Population = 80000 };
            context.Places.Add(place);
            await context.SaveChangesAsync();

            var service = new PlaceService(context);
            await service.DeletePlaceAsync(place.Id);

            Assert.Equal(0, await context.Places.CountAsync());
        }
    }


    //[Fact]
    //public async Task GetAverageHeightAsync_Should_Return_Correct_Average_Height()
    //{
    //    using (var context = new AppDbContext(_options))
    //    {
    //        context.Database.EnsureDeleted();
    //        context.Persons.AddRange(
    //            new Person
    //            {
    //                Name = "John",
    //                Surname = "Doe",
    //                BirthDate = new DateTime(1990, 1, 1),
    //                PersonalIdNumber = "1234567890123",
    //                Height = 180,
    //                PlaceOfBirthId = 1,
    //                PlaceOfResidenceId = 2
    //            },
    //            new Person
    //            {
    //                Name = "Jane",
    //                Surname = "Doe",
    //                BirthDate = new DateTime(1994, 2, 2),
    //                PersonalIdNumber = "9876543210987",
    //                Height = 160,
    //                PlaceOfBirthId = 1,
    //                PlaceOfResidenceId = 2
    //            }
    //        );

    //        await context.SaveChangesAsync();

    //        var service = new PlaceService(context);
    //        var avgHeight = await service.GetAverageHeightAsync(2);

    //        Assert.Equal(170, avgHeight);
    //    }
    //}

    //[Fact]
    //public async Task GetAverageAgeAsync_Should_Return_Correct_Average_Age()
    //{
    //    using (var context = new AppDbContext(_options))
    //    {
    //        context.Database.EnsureDeleted();
    //        context.Persons.AddRange(
    //            new Person
    //            {
    //                Name = "John",
    //                Surname = "Doe",
    //                BirthDate = new DateTime(1990, 1, 1),
    //                PersonalIdNumber = "1234567890123",
    //                Height = 180,
    //                PlaceOfBirthId = 1,
    //                PlaceOfResidenceId = 2
    //            },
    //            new Person
    //            {
    //                Name = "Jane",
    //                Surname = "Doe",
    //                BirthDate = new DateTime(1994, 2, 2),
    //                PersonalIdNumber = "9876543210987",
    //                Height = 160,
    //                PlaceOfBirthId = 1,
    //                PlaceOfResidenceId = 2
    //            }
    //        );

    //        await context.SaveChangesAsync();

    //        var service = new PlaceService(context);
    //        var avgAge = await service.GetAverageAgeAsync(2);

    //        Assert.Equal(32, Double.Round(avgAge));
    //    }
    //}
}
