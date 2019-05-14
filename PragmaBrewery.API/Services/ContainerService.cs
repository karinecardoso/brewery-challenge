using PragmaBrewery.API.Contracts.Containers;
using PragmaBrewery.API.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PragmaBrewery.API.Services
{
    public class ContainerService : IContainerService
    {
        private readonly IContainerRepository _containerRepository;

        public ContainerService(IContainerRepository containerRepository)
        {
            _containerRepository = containerRepository;
        }

        public async Task<List<ContainerDto>> Get()
        {
            var containers = await _containerRepository.Get();
            return containers.Select(x => new ContainerDto(x)).ToList();
        }

        public async Task<ContainerDto> GetById(long id)
        {
            var container = await _containerRepository.GetById(id);

            if (container == null)
            {
                return null;
            }

            return new ContainerDto(container);
        }
    }
}
