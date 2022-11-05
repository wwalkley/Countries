using Microsoft.AspNetCore.Mvc;

namespace Countries.Api.Controllers;

[ApiController]
[Route("api/countries")]
public sealed class Countries : ControllerBase
{
    /// <summary>
    /// Returns a list of Countries. 
    /// </summary>
    [HttpGet]
    public JsonResult GetCountries()
    {
        return new JsonResult(
            new List<object>
            {
                new { Id = 1, Name = "New Zealand" },
                new { Id = 2, Name = "Australia" }
            });
    }
}