using Countries.Api.Entities;

namespace Countries.Api.Repositories;

public interface IAppRepository
{
    Task<IEnumerable<City>> GetCities( );

    Task<City?> GetCity( int id );

    Task<IEnumerable<Country>> GetCountries( );

    Task<Country?> GetCountry( int id );
}