using Countries.Api.Models.City;
using Countries.Api.Stores;
using Microsoft.AspNetCore.Mvc;

namespace Countries.Api.Controllers;

[ApiController]
[Route( "api/cities" )]
public sealed class CityController : ControllerBase
{
    /// <summary>
    /// Returns a list of <see cref="City"/>
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<City>> GetCities( )
    {
        return Ok( DataStore.Data.Cities );
    }

    /// <summary>
    /// Returns a singular <see cref="City"/> based on provided Id.
    /// </summary>
    /// <param name="id">An Id for a specific City</param>
    [HttpGet( "{id}" )]
    public ActionResult<City> GetCity( int id )
    {
        var city = DataStore.Data.Cities.FirstOrDefault( c => c.Id == id );

        return city == default ? NotFound( ) : Ok( city );
    }
}