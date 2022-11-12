using Countries.Api.Models.Country;
using Countries.Api.Stores;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Countries.Api.Controllers;

[ApiController]
[Route( "api/countries" )]
public sealed class CountryController : ControllerBase
{
    private readonly ILogger<CountryController> _logger;

    public CountryController( ILogger<CountryController> logger )
    {
        _logger = logger;
    }

    /// <summary>
    /// Returns a list of <see cref="Country"/>
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Country>> GetCountries( )
    {
        return Ok( DataStore.Data.Countries );
    }

    /// <summary>
    /// Returns a singular <see cref="Country"/> based on provided Id.
    /// </summary>
    /// <param name="id">An Id for a specific Country</param>
    [HttpGet( "{id}", Name = "GetCountry" )]
    public ActionResult<Country> GetCountry( int id )
    {
        var country = DataStore.Data.Countries.FirstOrDefault( c => c.Id == id );

        if ( country == null )
        {
            LogNotFound( id );
            return NotFound( );
        }

        return Ok( country );
    }

    /// <summary>
    /// Creates a <see cref="Country"/>
    /// </summary>
    [HttpPost( "" )]
    public ActionResult<Country> Create( Country request )
    {
        var country = MapCountry( request );

        DataStore.Data.Countries.Add( country );

        _logger.LogInformation( "Created Country with id \'{CountryId}\'", country.Id );
        return CreatedAtRoute( "GetCountry", new { country.Id }, country );
    }


    /// <summary>
    /// Updates a <see cref="Country"/>
    /// </summary>
    [HttpPut( "{id}" )]
    public ActionResult<Country> Update( int id, Country request )
    {
        var country = DataStore.Data.Countries.FirstOrDefault( c => c.Id == id );

        if ( country == default )
        {
            LogNotFound( id );
            return NotFound( );
        }

        country.Name = request.Name;
        country.Capital = request.Capital;
        country.Population = request.Population;

        return NoContent( );
    }


    /// <summary>
    /// Patches a <see cref="Country"/>
    /// </summary>
    [HttpPatch( "{id}" )]
    public ActionResult<Country> PartialUpdate( int id, JsonPatchDocument<Country> patchDocument )
    {
        var country = DataStore.Data.Countries.FirstOrDefault( c => c.Id == id );

        if ( country == default )
        {
            LogNotFound( id );
            return NotFound( );
        }

        patchDocument.ApplyTo( country );

        return NoContent( );
    }


    /// <summary>
    /// Deletes a <see cref="Country"/>
    /// </summary>
    [HttpDelete( "{id}" )]
    public ActionResult Delete( int id )
    {
        var country = DataStore.Data.Countries.FirstOrDefault( c => c.Id == id );

        if ( country == default )
        {
            LogNotFound( id );
            return NotFound( );
        }

        DataStore.Data.Countries.Remove( country );

        return NoContent( );
    }

    private static Country MapCountry( Country request )
    {
        var country = new Country
        {
            Id = DataStore.Data.Countries.Select( c => c.Id ).Max( ) + 1,
            Name = request.Name,
            Population = request.Population,
            Capital = request.Capital
        };
        return country;
    }

    private void LogNotFound( int id )
    {
        _logger.LogInformation( "Country with Id \'{Id}\' was not found", id );
    }
}