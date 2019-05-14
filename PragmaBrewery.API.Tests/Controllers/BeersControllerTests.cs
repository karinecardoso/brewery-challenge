using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PragmaBrewery.API.Contracts.Beers;
using PragmaBrewery.API.Controllers;
using PragmaBrewery.API.Domain;
using PragmaBrewery.API.Resources.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PragmaBrewery.API.Tests.Controllers
{
    public class BeersControllerTests
    {
        private static readonly List<BeerDto> Beers = new List<BeerDto>
        {
            new BeerDto(new Beer(1, "Beer 1", BeerType.Pilsner, -6, -4)),
            new BeerDto(new Beer(2, "Beer 2", BeerType.IPA, -6, -5)),
            new BeerDto(new Beer(3, "Beer 3", BeerType.Lager, -7, -4)),
            new BeerDto(new Beer(4, "Beer 4", BeerType.Stout, -8, -6)),
            new BeerDto(new Beer(5, "Beer 5", BeerType.WheatBeer, -5, -3)),
            new BeerDto(new Beer(6, "Beer 6", BeerType.PaleAle, -6, -4))
        };

        private static readonly List<BeerContainerDto> BeersContainers = new List<BeerContainerDto>
        {
            new BeerContainerDto
            (
                new Beer(1, "Beer 1", BeerType.Pilsner, -6, -4),
                new Container(1, "Container 1", 1),
                new TemperatureAlert(TemperatureAlertType.Above, -3)
            ),
            new BeerContainerDto
            (
                new Beer(2, "Beer 2", BeerType.IPA, -6, -5),
                new Container(2, "Container 2", 2),
                new TemperatureAlert(TemperatureAlertType.Below, -7)
            ),
            new BeerContainerDto
            (
                new Beer(3, "Beer 3", BeerType.Lager, -7, -4),
                new Container(3, "Container 3", 3),
                null
            )
        };

        private static Mock<IBeerService> MockBeerService
        (
            List<BeerDto> beers = null,
            List<BeerContainerDto> beersContainers = null
        )
        {
            var mock = new Mock<IBeerService>();

            mock.Setup(x => x.Get()).ReturnsAsync(beers ?? new List<BeerDto>());
            mock.Setup(x => x.GetById(It.IsAny<long>()))
                .ReturnsAsync(beers?.Count > 0 ? beers[0] : null);
            mock.Setup(x => x.GetContainersTemperatures())
                .ReturnsAsync(beersContainers ?? new List<BeerContainerDto>());

            return mock;
        }

        [Fact]
        public async Task Should_get_beers()
        {
            var beerService = MockBeerService(Beers);

            var sut = new BeersController(
                beerService.Object
            );

            var result = await sut.GetBeers();
            var actionResult = Assert.IsType<ActionResult<IEnumerable<BeerDto>>>(result);

            beerService.Verify(x => x.Get(), Times.Once);

            actionResult.Value.Should().NotBeNull();
            actionResult.Value.Count().Should().Be(6);
            actionResult.Result.Should().BeNull();
        }

        [Fact]
        public async Task Should_get_beer_by_id_when_exists()
        {
            var id = 1;
            var beerService = MockBeerService(Beers);

            var sut = new BeersController(
                beerService.Object
            );

            var result = await sut.GetBeer(id);
            var actionResult = Assert.IsType<ActionResult<BeerDto>>(result);

            beerService.Verify(x => x.GetById(It.IsAny<long>()), Times.Once);

            actionResult.Value.Should().NotBeNull();
            actionResult.Value.Id.Should().Be(id);
            actionResult.Result.Should().BeNull();
        }

        [Fact]
        public async Task Should_return_not_found()
        {
            var id = 7;
            var beerService = MockBeerService();

            var sut = new BeersController(
                beerService.Object
            );

            var result = await sut.GetBeer(id);
            var actionResult = Assert.IsType<NotFoundResult>(result.Result);

            beerService.Verify(x => x.GetById(It.IsAny<long>()), Times.Once);

            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_get_beers_containers_temperatures()
        {
            var beerService = MockBeerService(Beers, BeersContainers);

            var sut = new BeersController(
                beerService.Object
            );

            var result = await sut.GetBeersContainersTemperatures();
            var actionResult = Assert.IsType<ActionResult<IEnumerable<BeerContainerDto>>>(result);

            beerService.Verify(x => x.GetContainersTemperatures(), Times.Once);

            actionResult.Value.Should().NotBeNull();
            actionResult.Value.Count().Should().Be(3);

            actionResult.Value.First().TemperatureAlert.Should().NotBeNull();
            actionResult.Value.Last().TemperatureAlert.Should().BeNull();

            actionResult.Result.Should().BeNull();
        }
    }
}
