using Microsoft.EntityFrameworkCore;
using PragmaBrewery.API.Domain;
using PragmaBrewery.API.Domain.Repositories;
using PragmaBrewery.API.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PragmaBrewery.API.Infrastructure.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly PragmaBreweryDbContext _dbContext;

        public BeerRepository(PragmaBreweryDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public async Task<List<Beer>> Get()
        {
            return await _dbContext.Beers.ToListAsync();
        }

        public async Task<Beer> GetById(long id)
        {
            return await _dbContext.Beers.FindAsync(id);
        }

        public async Task<List<Beer>> GetComplete()
        {
            return await _dbContext.Beers.Include(x => x.Container).ToListAsync();
        }
    }
}
