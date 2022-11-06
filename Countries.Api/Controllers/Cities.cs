using Countries.Api.Models;
using Countries.Api.Stores;
using Microsoft.AspNetCore.Mvc;

namespace Countries.Api.Controllers;

[ApiController]
[Route( "api/cities" )]
public sealed class Cities : ControllerBase
{
    private readonly DataStore _dataStore = new( );

    /// <summary>
    /// Returns a list of <see cref="City"/>
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<City>> GetCities( )
    {
        return Ok( _dataStore.Cities );
    }

    /// <summary>
    /// Returns a singular <see cref="City"/> based on provided Id.
    /// </summary>
    /// <param name="id">An Id for a specific City</param>
    [HttpGet( "{id}" )]
    public ActionResult<City> GetCity( int id )
    {
        var city = _dataStore.Cities.FirstOrDefault( c => c.Id == id );

        return city == default ? NotFound( ) : Ok( city );
    }
}