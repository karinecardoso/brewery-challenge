using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using PragmaBrewery.API.Domain;
using PragmaBrewery.API.Resources.Enums;

namespace PragmaBrewery.API.Infrastructure.Data
{
    public class PragmaBreweryDbContext : DbContext
    {
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Container> Containers { get; set; }

        public PragmaBreweryDbContext(DbContextOptions<PragmaBreweryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Beer>().ToTable("Beers");

            builder.Entity<Beer>().HasKey(x => x.Id);

            builder.Entity<Beer>().Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasValueGenerator<InMemoryIntegerValueGenerator<long>>();

            builder.Entity<Beer>().Property(x => x.Name).IsRequired();

            builder.Entity<Beer>().Property(x => x.BeerType).IsRequired();

            builder.Entity<Beer>().Property(x => x.MinTemperature).IsRequired();

            builder.Entity<Beer>().Property(x => x.MaxTemperature).IsRequired();

            builder.Entity<Beer>().HasData
            (
                new Beer { Id = 1, Name = "Beer 1", BeerType = BeerType.Pilsner, MinTemperature = -6, MaxTemperature = -4 },
                new Beer { Id = 2, Name = "Beer 2", BeerType = BeerType.IPA, MinTemperature = -6, MaxTemperature = -5 },
                new Beer { Id = 3, Name = "Beer 3", BeerType = BeerType.Lager, MinTemperature = -7, MaxTemperature = -4 },
                new Beer { Id = 4, Name = "Beer 4", BeerType = BeerType.Stout, MinTemperature = -8, MaxTemperature = -6 },
                new Beer { Id = 5, Name = "Beer 5", BeerType = BeerType.WheatBeer, MinTemperature = -5, MaxTemperature = -3 },
                new Beer { Id = 6, Name = "Beer 6", BeerType = BeerType.PaleAle, MinTemperature = -6, MaxTemperature = -4 }
            );

            builder.Entity<Container>().ToTable("Containers");

            builder.Entity<Container>().HasKey(x => x.Id);

            builder.Entity<Container>()
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasValueGenerator<InMemoryIntegerValueGenerator<long>>();

            builder.Entity<Container>().Property(x => x.Name).IsRequired();

            builder.Entity<Container>()
                .HasOne(x => x.Beer)
                .WithOne(x => x.Container)
                .HasForeignKey<Container>(x => x.IdBeer);

            builder.Entity<Container>().HasData
            (
                new Container { Id = 1, Name = "Container 1", IdBeer = 1 },
                new Container { Id = 2, Name = "Container 2", IdBeer = 2 },
                new Container { Id = 3, Name = "Container 3", IdBeer = 3 },
                new Container { Id = 4, Name = "Container 4", IdBeer = 4 },
                new Container { Id = 5, Name = "Container 5", IdBeer = 5 },
                new Container { Id = 6, Name = "Container 6", IdBeer = 6 }
            );
        }
    }
}
