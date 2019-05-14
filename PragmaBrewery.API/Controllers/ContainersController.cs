using Microsoft.AspNetCore.Mvc;
using PragmaBrewery.API.Contracts.Containers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PragmaBrewery.API.Controllers
{
    [Route("containers")]
    public class ContainersController : Controller
    {
        private readonly IContainerService _containerService;

        public ContainersController(IContainerService containerService)
        {
            _containerService = containerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContainerDto>>> GetContainers()
        {
            return await _containerService.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContainerDto>> GetContainer(long id)
        {
            var container = await _containerService.GetById(id);

            if (container == null)
            {
                return NotFound();
            }

            return container;
        }
    }
}
