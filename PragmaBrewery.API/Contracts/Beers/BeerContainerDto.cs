using PragmaBrewery.API.Contracts.Containers;
using PragmaBrewery.API.Domain;

namespace PragmaBrewery.API.Contracts.Beers
{
    public class BeerContainerDto
    {
        public BeerContainerDto()
        {
        }
        
        public BeerContainerDto(
            Beer beer,
            Container container,
            TemperatureAlert temperatureAlert
        )
        {
            Beer = new BeerDto(beer);
            Container = new ContainerDto(container);
            if (temperatureAlert != null)
            {
                TemperatureAlert = new TemperatureAlertDto(temperatureAlert);
            }
        }

        public BeerDto Beer { get; set; }
        public ContainerDto Container { get; set; }
        public TemperatureAlertDto TemperatureAlert { get; set; }
    }
}
