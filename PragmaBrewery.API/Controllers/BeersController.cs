using Microsoft.AspNetCore.Mvc;
using PragmaBrewery.API.Contracts.Beers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PragmaBrewery.API.Controllers
{
    [Route("beers")]
    public class BeersController : Controller
    {
        private readonly IBeerService _beerService;

        public BeersController(IBeerService beerService)
        {
            _beerService = beerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BeerDto>>> GetBeers()
        {
            return await _beerService.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetBeer(int id)
        {
            var beer = await _beerService.GetById(id);

            if (beer == null)
            {
                return NotFound();
            }

            return beer;
        }

        [HttpGet("containers-temperatures")]
        public async Task<ActionResult<IEnumerable<BeerContainerDto>>> GetBeersContainersTemperatures()
        {
            return await _beerService.GetContainersTemperatures();
        }
    }
}
