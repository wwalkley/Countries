namespace Countries.Api.Models.Country;

public sealed class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Capital { get; set; }
    public int Population { get; set; }
}