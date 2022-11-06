﻿using Countries.Api.Models;
using Countries.Api.Stores;
using Microsoft.AspNetCore.Mvc;

namespace Countries.Api.Controllers;

[ApiController]
[Route( "api/countries" )]
public sealed class Countries : ControllerBase
{
    private readonly DataStore _dataStore  = new( );

    /// <summary>
    /// Returns a list of <see cref="Country"/>
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Country>> GetCountries( )
    {
        return Ok( _dataStore.Countries );
    }

    /// <summary>
    /// Returns a singular <see cref="Country"/> based on provided Id.
    /// </summary>
    /// <param name="id">An Id for a specific Country</param>
    [HttpGet( "{id}" )]
    public ActionResult<Country> GetCountry( int id )
    {
        var country = _dataStore.Countries.FirstOrDefault( c => c.Id == id );

        return country == default ? NotFound( ) : Ok( country );
    }
}