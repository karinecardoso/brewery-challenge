using System.Collections.Generic;
using System.Threading.Tasks;

namespace PragmaBrewery.API.Domain.Repositories
{
    public interface IBeerRepository
    {
        Task<List<Beer>> Get();

        Task<Beer> GetById(long id);

        Task<List<Beer>> GetComplete();
    }
}
