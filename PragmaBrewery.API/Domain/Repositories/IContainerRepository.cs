using System.Collections.Generic;
using System.Threading.Tasks;

namespace PragmaBrewery.API.Domain.Repositories
{
    public interface IContainerRepository
    {
        Task<List<Container>> Get();

        Task<Container> GetById(long id);
    }
}
