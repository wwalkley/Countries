using Countries.Api.Models.Country;
using Countries.Api.Stores;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Countries.Api.Controllers;

[ApiController]
[Route( "api/countries" )]
public sealed class CountryController : ControllerBase
{
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

        return country == default ? NotFound( ) : Ok( country );
    }

    /// <summary>
    /// Creates a <see cref="Country"/>
    /// </summary>
    [HttpPost( "" )]
    public ActionResult<Country> Create( Country request )
    {
        var country = new Country
        {
            Id = DataStore.Data.Countries.Select( c => c.Id ).Max( ) + 1,
            Name = request.Name,
            Population = request.Population,
            Capital = request.Capital
        };

        DataStore.Data.Countries.Add( country );

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
            return NotFound( );
        }

        DataStore.Data.Countries.Remove( country );

        return NoContent( );
    }
}