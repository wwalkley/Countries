using Countries.Api.Entities;
using Countries.Api.Models;
using Countries.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Countries.Api.Controllers;

[ApiController]
[Route( "api/cities" )]
public sealed class CityController : ControllerBase
{
    private readonly IAppRepository _appRepository;
    private readonly ILogger<CityController> _logger;

    public CityController( IAppRepository appRepository, ILogger<CityController> logger )
    {
        _appRepository = appRepository;
        _logger = logger;
    }

    /// <summary>
    /// Returns a list of <see cref="City"/>
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CityDto>>> GetCities( )
    {
        try
        {
            return Ok( await _appRepository.GetCities( ) );
        }
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }

    /// <summary>
    /// Returns a singular <see cref="City"/> based on provided Id.
    /// </summary>
    /// <param name="id">An Id for a specific City</param>
    [HttpGet( "{id}", Name = "GetCity" )]
    public async Task<ActionResult<CityDto>> GetCity( int id )
    {
        try
        {
            var city = await _appRepository.GetCity( id );

            if ( city == null )
            {
                LogNotFound( id );
                return NotFound( );
            }

            return Ok( city );
        }
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }

    /// <summary>
    /// Creates a new <see cref="City"/>
    /// </summary>
    [HttpPost( "" )]
    public async Task<ActionResult<City>> Create( CityDto cityDto )
    {
        try
        {
            var city = CreateCity( cityDto );

            await _appRepository.AddCity( city );

            _logger.LogInformation( "Created City with id \'{CityId}\'", city.Id );
            return CreatedAtRoute( "GetCity", new { city.Id }, city );
        }
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }

    private static City CreateCity( CityDto cityDto )
    {
        return new City
        {
            Name = cityDto.Name,
            CountryId = cityDto.CountryId
        };
    }

    private void LogNotFound( int id )
    {
        _logger.LogInformation( "City with Id \'{Id}\' was not found", id );
    }

    private void LogError( Exception exception )
    {
        _logger.LogCritical( "Exception occured when executing function: {Exception}", exception );
    }
}