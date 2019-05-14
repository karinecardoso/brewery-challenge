using System.Collections.Generic;
using System.Threading.Tasks;

namespace PragmaBrewery.API.Contracts.Containers
{
    public interface IContainerService
    {
        Task<List<ContainerDto>> Get();

        Task<ContainerDto> GetById(long id);
    }
}
