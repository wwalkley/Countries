using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Countries.Api.Entities;

public class Country
{
    [Key]
    [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
    public int Id { get; set; }

    [Required]
    [MaxLength( 100 )]
    public string Name { get; set; }

    [Required]
    [MaxLength( 100 )]
    public string Capital { get; set; }

    [Required]
    public int Population { get; set; }
}