using System.ComponentModel;

namespace PragmaBrewery.API.Resources.Enums
{
    public enum TemperatureAlertType
    {
        [Description("Temperature is below the minimum.")]
        Below,
        [Description("Temperature is above maximum.")]
        Above
    }
}
