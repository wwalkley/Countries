using Countries.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Countries.Api.Stores;

public class Context : DbContext
{
    public Context( DbContextOptions<Context> options ) : base( options ) { }

    public DbSet<Country> Countries { get; set; } = null!;

    public DbSet<City> Cities { get; set; } = null!;

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Country>( ).HasData(
            new Country { Id = 1, Name = "New Zealand", Capital = "Wellington", Population = 5131480 },
            new Country { Id = 2, Name = "Australia", Capital = "Canberra", Population = 25890773 },
            new Country { Id = 3, Name = "England", Capital = "London", Population = 56489800 } );

        modelBuilder.Entity<City>( ).HasData(
            new City { Id = 1, Name = "Auckland", CountryId = 1 },
            new City { Id = 2, Name = "Wellington", CountryId = 1 },
            new City { Id = 3, Name = "Canberra", CountryId = 2 },
            new City { Id = 4, Name = "Melbourne", CountryId = 2 },
            new City { Id = 5, Name = "London", CountryId = 3 },
            new City { Id = 6, Name = "Manchester", CountryId = 3 } );

        base.OnModelCreating( modelBuilder );
    }
}