namespace Countries.Api.Models.Country;

public sealed class CreateCountry
{
    public string Name { get; set; }
    public string Capital { get; set; }
    public int Population { get; set; }
}