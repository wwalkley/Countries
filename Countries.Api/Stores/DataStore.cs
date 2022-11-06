using Countries.Api.Models.City;
using Countries.Api.Models.Country;

namespace Countries.Api.Stores;

public sealed class DataStore
{
    public static readonly DataStore Data = new( );

    private DataStore( )
    {
        Countries = GetCountriesData( );
        Cities = GetCitiesData( );
    }

    public List<Country> Countries { get; }
    public List<City> Cities { get; }

    /// <summary>
    /// Initial data store to test without database for Countries.
    /// </summary>
    private static List<Country> GetCountriesData( )
    {
        return new List<Country>
        {
            new( ) { Id = 1, Name = "New Zealand", Capital = "Wellington", Population = 5131480 },
            new( ) { Id = 2, Name = "Australia", Capital = "Canberra", Population = 25890773 },
            new( ) { Id = 3, Name = "England", Capital = "London", Population = 56489800 }
        };
    }

    /// <summary>
    /// Initial data store to test without database for Cities.
    /// </summary>
    private static List<City> GetCitiesData( )
    {
        return new List<City>
        {
            new( ) { Id = 1, Name = "Auckland", CountryId = 1 },
            new( ) { Id = 2, Name = "Wellington", CountryId = 1 },
            new( ) { Id = 3, Name = "Canberra", CountryId = 2 },
            new( ) { Id = 4, Name = "Melbourne", CountryId = 2 },
            new( ) { Id = 5, Name = "London", CountryId = 3 },
            new( ) { Id = 6, Name = "Manchester", CountryId = 3 }
        };
    }
}