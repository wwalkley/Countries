namespace Countries.Api.Models;

public sealed class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountryId { get; set; }
}