using Countries.Api.Models;

namespace Countries.Api.Stores;

public sealed class CountriesDataStore
{
    public List<Country> Countries { get; set; }

    public CountriesDataStore( )
    {
        Countries = GetCitiesData( );
    }

    /// <summary>
    /// Initial data store to test without database.
    /// </summary>
    private static List<Country> GetCitiesData( )
    {
        return new List<Country>
        {
            new Country { Id = 1, Name = "New Zealand", Capital = "Wellington", Population = 5131480 },
            new Country { Id = 2, Name = "Australia", Capital = "Canberra", Population = 25890773 },
            new Country { Id = 3, Name = "England", Capital = "London", Population = 56489800 }
        };
    }
}