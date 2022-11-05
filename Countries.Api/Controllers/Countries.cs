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
    /// Returns a list of Countries. 
    /// </summary>
    [HttpGet]
    public JsonResult GetCountries()
    {
        return new JsonResult(DummyStore.Countries);
    }
}