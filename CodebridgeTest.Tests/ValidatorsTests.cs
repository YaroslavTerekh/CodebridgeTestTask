using CodebridgeTest.BL.Behaviors.Dogs.CreateDog;
using CodebridgeTest.Domain.DbConnection;
using CodebridgeTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CodebridgeTest.Tests;

public class ValidatorsTests
{
    [Fact]
    public async Task CreateDogCommandValidatorShouldHaveValidationErrorFor_WhenNameIsNull()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "MoqDatabase")
            .Options;

        using (var context = new DataContext(options))
        {
            var validator = new CreateDogCommandValidator(context);            

            var result = await validator.ValidateAsync(new CreateDogCommand { Name = null, Color = "", TailLength = 90, Weight = 100 });

            Assert.NotNull(result.Errors.Where(t => t.PropertyName == "Name"));
        }
    }
}
