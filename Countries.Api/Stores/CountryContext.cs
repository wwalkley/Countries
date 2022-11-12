using Countries.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Countries.Api.Stores;

public class CountryContext : DbContext
{
    public CountryContext( DbContextOptions<CountryContext> options ) : base( options )
    {
    }

    public DbSet<Country> Countries { get; set; } = null!;

    public DbSet<City> Cities { get; set; } = null!;
}