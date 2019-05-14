using FluentAssertions;
using Moq;
using PragmaBrewery.API.Domain;
using PragmaBrewery.API.Domain.Repositories;
using PragmaBrewery.API.Resources.Enums;
using PragmaBrewery.API.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PragmaBrewery.API.Tests.Services
{
    public class BeerServiceTests
    {
        private static readonly List<Beer> Beers = new List<Beer>
        {
            new Beer(1, "Beer 1", BeerType.Pilsner, -6, -4),
            new Beer(2, "Beer 2", BeerType.IPA, -6, -5),
            new Beer(3, "Beer 3", BeerType.Lager, -7, -4),
            new Beer(4, "Beer 4", BeerType.Stout, -8, -6),
            new Beer(5, "Beer 5", BeerType.WheatBeer, -5, -3),
            new Beer(6, "Beer 6", BeerType.PaleAle, -6, -4)
        };

        private static readonly List<Beer> FullBeers = new List<Beer>
        {
            new Beer
            {
                Id = 1, Name = "Beer 1", BeerType = BeerType.Pilsner, MinTemperature = -6, MaxTemperature = -4,
                Container = new Container(1, "Container 1", 1),
            },
            new Beer
            {
                Id = 2, Name = "Beer 2", BeerType = BeerType.IPA, MinTemperature = -6, MaxTemperature = -5,
                Container = new Container(2, "Container 2", 2)
            }
        };

        private static Mock<IBeerRepository> MockBeerRepository(List<Beer> beers = null, List<Beer> fullBeers = null)
        {
            var mock = new Mock<IBeerRepository>();

            mock.Setup(x => x.Get()).ReturnsAsync(beers ?? new List<Beer>());
            mock.Setup(x => x.GetById(It.IsAny<long>()))
                .ReturnsAsync(beers?.Count > 0 ? beers[0] : null);
            mock.Setup(x => x.GetComplete()).ReturnsAsync(fullBeers ?? new List<Beer>());

            return mock;
        }

        [Fact]
        public void Should_invoke_service_and_return_beers()
        {
            var beerRepository = MockBeerRepository(Beers);

            var sut = new BeerService(
                beerRepository.Object
            );

            var result = sut.Get();

            beerRepository.Verify(x => x.Get(), Times.Once);

            result.Result.Should().NotBeNull();
            result.Result.Count.Should().Be(6);
        }

        [Fact]
        public void Should_invoke_service_and_return_beer_by_id_when_exists()
        {
            var id = 1;
            var beerRepository = MockBeerRepository(Beers);

            var sut = new BeerService(
                beerRepository.Object
            );

            var result = sut.GetById(id);

            beerRepository.Verify(x => x.GetById(It.IsAny<long>()), Times.Once);

            result.Result.Should().NotBeNull();
            result.Result.Id = id;
        }

        [Fact]
        public void Should_invoke_service_and_return_null_when_beer_not_exists()
        {
            var id = 7;
            var beerRepository = MockBeerRepository();

            var sut = new BeerService(
                beerRepository.Object
            );

            var result = sut.GetById(id);

            beerRepository.Verify(x => x.GetById(It.IsAny<long>()), Times.Once);

            result.Result.Should().BeNull();
        }

        [Fact]
        public void Should_invoke_service_and_return_beers_containers_temperatures()
        {
            var beerRepository = MockBeerRepository(Beers, FullBeers);

            var sut = new BeerService(
                beerRepository.Object
            );

            var result = sut.GetContainersTemperatures();

            beerRepository.Verify(x => x.GetComplete(), Times.Once);

            result.Result.Should().NotBeNull();
            result.Result.Count.Should().Be(2);
            result.Result.First().Container.Should().NotBeNull();
        }
    }
}
