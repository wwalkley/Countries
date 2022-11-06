namespace Countries.Api.Models.Country;

public sealed class CreateOrUpdateCountryDto
{
    public string Name { get; set; }
    public string Capital { get; set; }
    public int Population { get; set; }
}