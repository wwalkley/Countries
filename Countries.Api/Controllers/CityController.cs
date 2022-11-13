using Countries.Api.Models;
using Countries.Api.Stores;
using Microsoft.AspNetCore.Mvc;

namespace Countries.Api.Controllers;

[ApiController]
[Route( "api/cities" )]
public sealed class CityController : ControllerBase
{
    private readonly DataStore _dataStore;

    public CityController( DataStore dataStore )
    {
        _dataStore = dataStore;
    }

    /// <summary>
    /// Returns a list of <see cref="CityDto"/>
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<CityDto>> GetCities( )
    {
        return Ok( _dataStore.Cities );
    }

    /// <summary>
    /// Returns a singular <see cref="CityDto"/> based on provided Id.
    /// </summary>
    /// <param name="id">An Id for a specific City</param>
    [HttpGet( "{id}" )]
    public ActionResult<CityDto> GetCity( int id )
    {
        var city = _dataStore.Cities.FirstOrDefault( c => c.Id == id );

        return city == default ? NotFound( ) : Ok( city );
    }
}