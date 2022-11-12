using Countries.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Countries.Api.Stores;

public class Context : DbContext
{
    public Context( DbContextOptions<Context> options ) : base( options ) { }

    public DbSet<Country> Countries { get; set; } = null!;

    public DbSet<City> Cities { get; set; } = null!;
}