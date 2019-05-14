using PragmaBrewery.API.Resources.Enums;

namespace PragmaBrewery.API.Domain
{
    public class TemperatureAlert
    {
        public TemperatureAlert(TemperatureAlertType temperatureAlertType, int temperature)
        {
            TemperatureAlertType = temperatureAlertType;
            Temperature = temperature;
        }

        public TemperatureAlertType TemperatureAlertType { get; }
        public int Temperature { get; }
    }
}
