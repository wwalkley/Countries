using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Countries.Api.Entities;

public class City
{
    [Key]
    [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
    public int Id { get; set; }


    [Required]
    [MaxLength( 100 )]
    public string Name { get; set; }

    [Required]
    public Country? Country { get; set; }

    [Required]
    public int CountryId { get; set; }
}