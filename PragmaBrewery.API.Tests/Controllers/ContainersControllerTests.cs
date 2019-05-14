using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PragmaBrewery.API.Contracts.Containers;
using PragmaBrewery.API.Controllers;
using PragmaBrewery.API.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PragmaBrewery.API.Tests.Controllers
{
    public class ContainersControllerTests
    {
        private static readonly List<ContainerDto> Containers = new List<ContainerDto>
        {
            new ContainerDto(new Container(1, "Container 1", 1)),
            new ContainerDto(new Container(2, "Container 2", 2)),
            new ContainerDto(new Container(3, "Container 3", 3)),
            new ContainerDto(new Container(4, "Container 4", 4)),
            new ContainerDto(new Container(5, "Container 5", 5)),
            new ContainerDto(new Container(6, "Container 6", 6))
        };

        private static Mock<IContainerService> MockContainerService(List<ContainerDto> containers = null)
        {
            var mock = new Mock<IContainerService>();

            mock.Setup(x => x.Get()).ReturnsAsync(containers ?? new List<ContainerDto>());
            mock.Setup(x => x.GetById(It.IsAny<long>()))
                .ReturnsAsync(containers?.Count > 0 ? containers[0] : null);

            return mock;
        }

        [Fact]
        public async Task Should_get_containers()
        {
            var containerService = MockContainerService(Containers);

            var sut = new ContainersController(
                containerService.Object
            );

            var result = await sut.GetContainers();
            var actionResult = Assert.IsType<ActionResult<IEnumerable<ContainerDto>>>(result);

            containerService.Verify(x => x.Get(), Times.Once);

            actionResult.Value.Should().NotBeNull();
            actionResult.Value.Count().Should().Be(6);
            actionResult.Result.Should().BeNull();
        }

        [Fact]
        public async Task Should_get_container_by_id_when_exists()
        {
            var id = 1;
            var containerService = MockContainerService(Containers);

            var sut = new ContainersController(
                containerService.Object
            );

            var result = await sut.GetContainer(id);
            var actionResult = Assert.IsType<ActionResult<ContainerDto>>(result);

            containerService.Verify(x => x.GetById(It.IsAny<long>()), Times.Once);

            actionResult.Value.Should().NotBeNull();
            actionResult.Value.Id.Should().Be(id);
            actionResult.Result.Should().BeNull();
        }

        [Fact]
        public async Task Should_return_not_found()
        {
            var id = 7;
            var containerService = MockContainerService();

            var sut = new ContainersController(
                containerService.Object
            );

            var result = await sut.GetContainer(id);
            var actionResult = Assert.IsType<NotFoundResult>(result.Result);

            containerService.Verify(x => x.GetById(It.IsAny<long>()), Times.Once);

            actionResult.Should().NotBeNull();
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
    }
}
