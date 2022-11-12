using Countries.Api.Models.Country;
using Countries.Api.Stores;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Countries.Api.Controllers;

[ApiController]
[Route( "api/countries" )]
public sealed class CountryController : ControllerBase
{
    private readonly DataStore _dataStore;
    private readonly ILogger<CountryController> _logger;

    public CountryController( ILogger<CountryController> logger, DataStore dataStore )
    {
        _logger = logger;
        _dataStore = dataStore;
    }

    /// <summary>
    /// Returns a list of <see cref="Country"/>
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Country>> GetCountries( )
    {
        try
        {
            return Ok( _dataStore.Countries );
        }
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }

    /// <summary>
    /// Returns a singular <see cref="Country"/> based on provided Id.
    /// </summary>
    /// <param name="id">An Id for a specific Country</param>
    [HttpGet( "{id}", Name = "GetCountry" )]
    public ActionResult<Country> GetCountry( int id )
    {
        try
        {
            var country = _dataStore.Countries.FirstOrDefault( c => c.Id == id );

            if ( country == null )
            {
                LogNotFound( id );
                return NotFound( );
            }

            return Ok( country );
        }
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }

    /// <summary>
    /// Creates a <see cref="Country"/>
    /// </summary>
    [HttpPost( "" )]
    public ActionResult<Country> Create( Country request )
    {
        try
        {
            var country = MapCountry( request );

            _dataStore.Countries.Add( country );

            _logger.LogInformation( "Created Country with id \'{CountryId}\'", country.Id );
            return CreatedAtRoute( "GetCountry", new { country.Id }, country );
        }
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }


    /// <summary>
    /// Updates a <see cref="Country"/>
    /// </summary>
    [HttpPut( "{id}" )]
    public ActionResult<Country> Update( int id, Country request )
    {
        try
        {
            var country = _dataStore.Countries.FirstOrDefault( c => c.Id == id );

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
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }


    /// <summary>
    /// Patches a <see cref="Country"/>
    /// </summary>
    [HttpPatch( "{id}" )]
    public ActionResult<Country> PartialUpdate( int id, JsonPatchDocument<Country> patchDocument )
    {
        try
        {
            var country = _dataStore.Countries.FirstOrDefault( c => c.Id == id );

            if ( country == default )
            {
                LogNotFound( id );
                return NotFound( );
            }

            patchDocument.ApplyTo( country );

            return NoContent( );
        }
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }


    /// <summary>
    /// Deletes a <see cref="Country"/>
    /// </summary>
    [HttpDelete( "{id}" )]
    public ActionResult Delete( int id )
    {
        try
        {
            var country = _dataStore.Countries.FirstOrDefault( c => c.Id == id );

            if ( country == default )
            {
                LogNotFound( id );
                return NotFound( );
            }

            _dataStore.Countries.Remove( country );

            return NoContent( );
        }
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }


    private Country MapCountry( Country request )
    {
        return new Country
        {
            Id = _dataStore.Countries.Select( c => c.Id ).Max( ) + 1,
            Name = request.Name,
            Population = request.Population,
            Capital = request.Capital
        };
    }

    private void LogNotFound( int id )
    {
        _logger.LogInformation( "Country with Id \'{Id}\' was not found", id );
    }

    private void LogError( Exception exception )
    {
        _logger.LogCritical( "Exception occured when executing function: {Exception}", exception );
    }
}