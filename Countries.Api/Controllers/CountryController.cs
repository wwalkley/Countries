using Countries.Api.Entities;
using Countries.Api.Models;
using Countries.Api.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Countries.Api.Controllers;

[ApiController]
[Route( "api/countries" )]
public sealed class CountryController : ControllerBase
{
    private readonly IAppRepository _appRepository;
    private readonly ILogger<CountryController> _logger;

    public CountryController( ILogger<CountryController> logger, IAppRepository appRepository )
    {
        _logger = logger;
        _appRepository = appRepository;
    }

    /// <summary>
    /// Returns a list of <see cref="Country"/>
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries( )
    {
        try
        {
            return Ok( await _appRepository.GetCountries( ) );
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
    public async Task<ActionResult<CountryDto>> GetCountry( int id )
    {
        try
        {
            var country = await _appRepository.GetCountry( id );

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
    public async Task<ActionResult<CountryDto>> Create( CountryDto request )
    {
        try
        {
            var country = CreateCountry( request );

            await _appRepository.AddCountry( country );

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
    public async Task<ActionResult> Update( int id, CountryDto request )
    {
        try
        {
            var country = await _appRepository.GetCountry( id );

            if ( country == null )
            {
                LogNotFound( id );
                return NotFound( );
            }

            country.Name = request.Name;
            country.Capital = request.Capital;
            country.Population = request.Population;

            await _appRepository.SaveChangesAsync( );
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
    public async Task<ActionResult<CountryDto>> PartialUpdate( int id, JsonPatchDocument<CountryDto> patchDocument )
    {
        try
        {
            var country = await _appRepository.GetCountry( id );

            if ( country == default )
            {
                LogNotFound( id );
                return NotFound( );
            }

            // map domain entity to dto model
            var countryDto = MapEntityModelToDtoModel( country );

            // apply patch to dto
            patchDocument.ApplyTo( countryDto );

            // map patched dto model back into domain model
            ApplyUpdateMapping( country, countryDto );

            await _appRepository.SaveChangesAsync( );
            return NoContent( );
        }
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }


    /// <summary>
    /// Deletes a <see cref="CountryDto"/>
    /// </summary>
    [HttpDelete( "{id}" )]
    public async Task<ActionResult> Delete( int id )
    {
        try
        {
            var country = await _appRepository.GetCountry( id );

            if ( country == default )
            {
                LogNotFound( id );
                return NotFound( );
            }

            await _appRepository.DeleteCountry( country );

            return NoContent( );
        }
        catch ( Exception e )
        {
            LogError( e );
            return Problem( );
        }
    }


    private static Country CreateCountry( CountryDto request )
    {
        return new Country
        {
            Name = request.Name,
            Population = request.Population,
            Capital = request.Capital
        };
    }


    private static void ApplyUpdateMapping( Country country, CountryDto countryDto )
    {
        country.Name = countryDto.Name;
        country.Capital = countryDto.Capital;
        country.Population = countryDto.Population;
    }

    private static CountryDto MapEntityModelToDtoModel( Country country )
    {
        return new CountryDto
        {
            Name = country.Name,
            Capital = country.Capital,
            Population = country.Population
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