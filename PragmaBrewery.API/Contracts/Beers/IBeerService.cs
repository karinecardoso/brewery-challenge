using System.Collections.Generic;
using System.Threading.Tasks;

namespace PragmaBrewery.API.Contracts.Beers
{
    public interface IBeerService
    {
        Task<List<BeerDto>> Get();

        Task<BeerDto> GetById(long id);

        Task<List<BeerContainerDto>> GetContainersTemperatures();
    }
}
