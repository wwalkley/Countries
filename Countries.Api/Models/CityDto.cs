namespace Countries.Api.Models;

public sealed class CityDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountryId { get; set; }
}