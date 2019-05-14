using Microsoft.EntityFrameworkCore;
using PragmaBrewery.API.Domain;
using PragmaBrewery.API.Domain.Repositories;
using PragmaBrewery.API.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PragmaBrewery.API.Infrastructure.Repositories
{
    public class ContainerRepository : IContainerRepository
    {
        private readonly PragmaBreweryDbContext _dbContext;

        public ContainerRepository(PragmaBreweryDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public async Task<List<Container>> Get()
        {
            return await _dbContext.Containers.ToListAsync();
        }

        public async Task<Container> GetById(long id)
        {
            return await _dbContext.Containers.FindAsync(id);
        }
    }
}
