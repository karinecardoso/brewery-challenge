using System.ComponentModel;

namespace PragmaBrewery.API.Resources.Enums
{
    public enum BeerType
    {
        [Description("Pilsner")]
        Pilsner = 0,
        [Description("IPA")]
        IPA = 1,
        [Description("Lager")]
        Lager = 2,
        [Description("Stout")]
        Stout = 3,
        [Description("Wheat beer")]
        WheatBeer = 4,
        [Description("Pale Ale")]
        PaleAle = 5
    }
}
