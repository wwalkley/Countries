using Countries.Api.Models;
using Countries.Api.Stores;
using Microsoft.AspNetCore.Mvc;

namespace Countries.Api.Controllers;

[ApiController]
[Route("api/countries")]
public sealed class Countries : ControllerBase
{
    private CountriesDataStore DummyStore { get; set; } = new();

    /// <summary>
    /// Returns a list of <see cref="Country"/>
    /// </summary>
    [HttpGet]
    public JsonResult GetCountries()
    {
        return new JsonResult(DummyStore.Countries);
    }

    /// <summary>
    /// Returns a singular <see cref="Country"/> based on provided Id.
    /// </summary>
    /// <param name="id">An Id for a specific Country</param>
    [HttpGet("{id}")]
    public JsonResult GetCountry(int id)
    {
        var country = DummyStore.Countries.FirstOrDefault(c => c.Id == id);

        return country == default ? new JsonResult(new { }) : new JsonResult(country);
    }
}