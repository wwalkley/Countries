using Countries.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Countries.Api.Stores;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext( DbContextOptions<DbContext> options ) : base( options )
    {
    }

    public DbSet<Country> Countries { get; set; } = null!;

    public DbSet<City> Cities { get; set; } = null!;
}