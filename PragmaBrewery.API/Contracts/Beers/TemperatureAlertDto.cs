using PragmaBrewery.API.Domain;
using PragmaBrewery.API.Resources.Enums;

namespace PragmaBrewery.API.Contracts.Beers
{
    public class TemperatureAlertDto
    {
        public TemperatureAlertDto()
        {
        }

        public TemperatureAlertDto(TemperatureAlert temperatureAlert)
        {
            TemperatureAlertTypeId = temperatureAlert.TemperatureAlertType;
            TemperatureAlertType = TemperatureAlertTypeId.GetDescription();
            Temperature = temperatureAlert.Temperature;
        }

        public TemperatureAlertType TemperatureAlertTypeId { get; set; }
        public string TemperatureAlertType { get; set; }
        public int Temperature { get; set; }
    }
}
