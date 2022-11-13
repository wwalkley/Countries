using Countries.Api.Entities;

namespace Countries.Api.Repositories;

public interface IAppRepository
{
    Task<IEnumerable<City>> GetCities( );

    Task<City?> GetCity( int id );

    Task<IEnumerable<Country>> GetCountries( );

    Task<Country?> GetCountry( int id );

    Task AddCountry( Country country );

    Task DeleteCountry( Country country );

    Task AddCity( City country );

    Task<bool> SaveChangesAsync( );
}