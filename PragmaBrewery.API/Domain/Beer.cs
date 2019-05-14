using PragmaBrewery.API.Resources.Enums;

namespace PragmaBrewery.API.Domain
{
    public class Beer
    {
        public Beer()
        {
        }

        public Beer(string name, BeerType beerType, int minTemperature, int maxTemperature)
        {
            Name = name;
            BeerType = beerType;
            MinTemperature = minTemperature;
            MaxTemperature = maxTemperature;
        }

        public Beer(long id, string name, BeerType beerType, int minTemperature, int maxTemperature)
        {
            Id = id;
            Name = name;
            BeerType = beerType;
            MinTemperature = minTemperature;
            MaxTemperature = maxTemperature;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public BeerType BeerType { get; set; }
        public int MinTemperature { get; set; }
        public int MaxTemperature { get; set; }

        public Container Container { get; set; }
    }
}
