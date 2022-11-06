using Countries.Api.Models;

namespace Countries.Api.Stores;

public sealed class DataStore
{
    public List<Country> Countries { get; set; }
    public List<City> Cities { get; set; }

    public DataStore( )
    {
        Countries = GetCountriesData( );
        Cities = GetCitiesData( );
    }

    /// <summary>
    /// Initial data store to test without database for Countries.
    /// </summary>
    private static List<Country> GetCountriesData( )
    {
        return new List<Country>
        {
            new Country { Id = 1, Name = "New Zealand", Capital = "Wellington", Population = 5131480 },
            new Country { Id = 2, Name = "Australia", Capital = "Canberra", Population = 25890773 },
            new Country { Id = 3, Name = "England", Capital = "London", Population = 56489800 }
        };
    }

    /// <summary>
    /// Initial data store to test without database for Cities.
    /// </summary>
    private static List<City> GetCitiesData( )
    {
        return new List<City>
        {
            new City { Id = 1, Name = "Auckland", CountryId = 1 },
            new City { Id = 2, Name = "Wellington", CountryId = 1 },
            new City { Id = 3, Name = "Canberra", CountryId = 2 },
            new City { Id = 4, Name = "Melbourne", CountryId = 2 },
            new City { Id = 5, Name = "London", CountryId = 3 },
            new City { Id = 6, Name = "Manchester", CountryId = 3 }
        };
    }
}