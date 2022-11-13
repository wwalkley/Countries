using Countries.Api.Entities;
using Countries.Api.Stores;
using Microsoft.EntityFrameworkCore;

namespace Countries.Api.Repositories;

public sealed class AppRepository : IAppRepository
{
    private readonly Context _dbContext;


    public AppRepository( Context dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<City>> GetCities( )
    {
        return await _dbContext.Cities.OrderBy( c => c.Id ).ToListAsync( );
    }

    public async Task<City?> GetCity( int id )
    {
        return await _dbContext.Cities.Where( c => c.Id == id ).FirstOrDefaultAsync( );
    }

    public async Task<IEnumerable<Country>> GetCountries( )
    {
        return await _dbContext.Countries.OrderBy( c => c.Id ).ToListAsync( );
    }

    public async Task<Country?> GetCountry( int id )
    {
        return await _dbContext.Countries.Where( c => c.Id == id ).FirstOrDefaultAsync( );
    }
}