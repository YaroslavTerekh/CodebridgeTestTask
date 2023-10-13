using AutoMapper;
using CodebridgeTest.BL.Behaviors.Dogs.CreateDog;
using CodebridgeTest.BL.Behaviors.Dogs.GetAllDogs;
using CodebridgeTest.BL.Helpers;
using CodebridgeTest.Domain.DataTransferObjects;
using CodebridgeTest.Domain.DbConnection;
using CodebridgeTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodebridgeTest.Tests;

public class BehaviorsTests
{
    private readonly DbContextOptions<DataContext> _options;

    public BehaviorsTests()
    {
        _options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "MoqDatabase")
            .Options;
    }

    [Fact]
    public async Task CreateDogBehaviorCreatesObjectInDatabase()
    {
        var name = "Jack";

        using (var context = new DataContext(_options))
        {
            var handler = new CreateDogHandler(context);
            await handler.Handle(new CreateDogCommand() { Name = name, Color = "Red", Weight = 10, TailLength = 25 }, default);

            var result = await context.Dogs.FirstOrDefaultAsync(t => t.Name == name);

            Assert.NotNull(result);
        }
    }

    [Fact]
    public async Task GetAllDogsBehaviorReturnsCorrectDataByWeightAttributeAndDescOrder_BalysExpected()
    {
        using (var context = new DataContext(_options))
        {
            await MoqData(context);
            var myProfile = new MapperGlobalProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var handler = new GetAllDogsHandler(context, mapper);
            var resultData = await handler.Handle(new GetAllDogsQuery("weight", "desc", null, null), default);
            var result = resultData.Items[0].Name;

            Assert.Equal("Balys", result);
        }
    }

    [Fact]
    public async Task GetAllDogsBehaviorReturnsCorrectDataByTailLengthAttributeAndDescOrder_JackExpected()
    {
        using (var context = new DataContext(_options))
        {
            await MoqData(context);
            var myProfile = new MapperGlobalProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var handler = new GetAllDogsHandler(context, mapper);
            var resultData = await handler.Handle(new GetAllDogsQuery("tail_lenght", "desc", null, null), default);
            var result = resultData.Items[0].Name;

            Assert.Equal("Jack", result);
        }
    }

    [Fact]
    public async Task GetAllDogsBehaviorReturnsCorrectDataByColorAttributeAndDescOrder_BalysExpected()
    {
        using (var context = new DataContext(_options))
        {
            await MoqData(context);
            var myProfile = new MapperGlobalProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var handler = new GetAllDogsHandler(context, mapper);
            var resultData = await handler.Handle(new GetAllDogsQuery("color", "desc", null, null), default);
            var result = resultData.Items[0].Name;

            Assert.Equal("Balys", result);
        }
    }

    [Fact]
    public async Task GetAllDogsBehaviorReturnsCorrectDataByWeightNameAndDescOrder_JackExpected()
    {
        using (var context = new DataContext(_options))
        {
            await MoqData(context);
            var myProfile = new MapperGlobalProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var handler = new GetAllDogsHandler(context, mapper);
            var resultData = await handler.Handle(new GetAllDogsQuery("name", "desc", null, null), default);
            var result = resultData.Items[0].Name;

            Assert.Equal("Jack", result);
        }
    }

    [Fact]
    public async Task GetAllDogsBehaviorReturnsCorrectDataByPagination_OneEntityExpected()
    {
        using (var context = new DataContext(_options))
        {
            await MoqData(context);
            var myProfile = new MapperGlobalProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var handler = new GetAllDogsHandler(context, mapper);
            var resultData = await handler.Handle(new GetAllDogsQuery(null, null, 0, 1), default);
            var result = resultData.Items.Count;

            Assert.Equal(1, result);
        }
    }

    private async Task MoqData(DataContext context)
    {
        await context.Dogs.AddAsync(new Dog { Name = "Jack", Color = "Red", Weight = 10, TailLength = 25 });
        await context.Dogs.AddAsync(new Dog { Name = "Balys", Color = "White", Weight = 25, TailLength = 35 });

        await context.SaveChangesAsync();
    }
}
