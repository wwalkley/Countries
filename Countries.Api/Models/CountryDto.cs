namespace Countries.Api.Models;

public sealed class CountryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Capital { get; set; }
    public int Population { get; set; }
}