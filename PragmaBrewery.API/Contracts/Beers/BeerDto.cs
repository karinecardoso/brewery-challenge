using PragmaBrewery.API.Domain;
using PragmaBrewery.API.Resources.Enums;

namespace PragmaBrewery.API.Contracts.Beers
{
    public class BeerDto
    {
        public BeerDto()
        {
        }

        public BeerDto(Beer beer)
        {
            Id = beer.Id;
            Name = beer.Name;
            BeerTypeId = beer.BeerType;
            BeerType = BeerTypeId.GetDescription();
            MinTemperature = beer.MinTemperature;
            MaxTemperature = beer.MaxTemperature;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public BeerType BeerTypeId { get; set; }
        public string BeerType { get; set; }
        public int MinTemperature { get; set; }
        public int MaxTemperature { get; set; }
    }
}
