using FluentAssertions;
using Moq;
using PragmaBrewery.API.Domain;
using PragmaBrewery.API.Domain.Repositories;
using PragmaBrewery.API.Services;
using System.Collections.Generic;
using Xunit;

namespace PragmaBrewery.API.Tests.Services
{
    public class ContainerServiceTests
    {
        private static readonly List<Container> Containers = new List<Container>
        {
            new Container(1, "Container 1", 1),
            new Container(2, "Container 2", 2),
            new Container(3, "Container 3", 3),
            new Container(4, "Container 4", 4),
            new Container(5, "Container 5", 5),
            new Container(6, "Container 6", 6)
        };

        private static Mock<IContainerRepository> MockContainerService(List<Container> containers)
        {
            var mock = new Mock<IContainerRepository>();

            mock.Setup(x => x.Get()).ReturnsAsync(containers);
            mock.Setup(x => x.GetById(It.IsAny<long>())).ReturnsAsync(containers.Count > 0 ? containers[0] : null);

            return mock;
        }

        [Fact]
        public void Should_invoke_service_and_return_containers()
        {
            var containerService = MockContainerService(Containers);

            var sut = new ContainerService(
                containerService.Object
            );

            var result = sut.Get();

            containerService.Verify(x => x.Get(), Times.Once);

            result.Result.Should().NotBeNull();
            result.Result.Count.Should().Be(6);
        }

        [Fact]
        public void Should_invoke_service_and_return_container_by_id_when_exists()
        {
            var id = 1;
            var containerService = MockContainerService(Containers);

            var sut = new ContainerService(
                containerService.Object
            );

            var result = sut.GetById(id);

            containerService.Verify(x => x.GetById(It.IsAny<long>()), Times.Once);

            result.Result.Should().NotBeNull();
            result.Result.Id = id;
        }

        [Fact]
        public void Should_invoke_service_and_return_null_when_container_not_exists()
        {
            var id = 7;
            var containerService = MockContainerService(new List<Container>());

            var sut = new ContainerService(
                containerService.Object
            );

            var result = sut.GetById(id);

            containerService.Verify(x => x.GetById(It.IsAny<long>()), Times.Once);

            result.Result.Should().BeNull();
        }
    }
}
