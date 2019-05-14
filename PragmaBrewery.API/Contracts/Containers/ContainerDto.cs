using PragmaBrewery.API.Domain;

namespace PragmaBrewery.API.Contracts.Containers
{
    public class ContainerDto
    {
        public ContainerDto()
        {
        }

        public ContainerDto(Container container)
        {
            Id = container.Id;
            Name = container.Name;
            IdBeer = container.IdBeer;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long IdBeer { get; set; }
    }
}
