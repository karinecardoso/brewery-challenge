using PragmaBrewery.API.Contracts.Beers;
using PragmaBrewery.API.Domain;
using PragmaBrewery.API.Domain.Repositories;
using PragmaBrewery.API.Resources.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PragmaBrewery.API.Services
{
    public class BeerService : IBeerService
    {
        private readonly IBeerRepository _beerRepository;

        public BeerService(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }

        public async Task<List<BeerDto>> Get()
        {
            var beers = await _beerRepository.Get();
            return beers.Select(x => new BeerDto(x)).ToList();
        }

        public async Task<BeerDto> GetById(long id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer == null)
            {
                return null;
            }

            return new BeerDto(beer);
        }

        public async Task<List<BeerContainerDto>> GetContainersTemperatures()
        {
            var beers = await _beerRepository.GetComplete();
            return beers.Select(x => new BeerContainerDto(x, x.Container, GetTemperatureAlert(x))).ToList();
        }

        private TemperatureAlert GetTemperatureAlert(Beer beer)
        {
            var temperature = GetActualTemperatureContainer(beer);

            if (temperature < beer.MinTemperature)
            {
                return new TemperatureAlert(TemperatureAlertType.Below, temperature);
            }

            if (temperature > beer.MaxTemperature)
            {
                return new TemperatureAlert(TemperatureAlertType.Above, temperature);
            }

            return null;
        }

        private int GetActualTemperatureContainer(Beer beer)
        {
            return new Random().Next(beer.MinTemperature - 1, beer.MaxTemperature + 2);
        }
    }
}
