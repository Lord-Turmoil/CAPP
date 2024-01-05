using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Server.Modules.Core.Models;

[Owned]
public class Procedure : TimestampModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(1023)]
    public string Description { get; set; } = null!;

    [Required]
    public int Order { get; set; }

    [Required]
    public int PartId { get; set; }

    [ForeignKey("PartId")]
    public Part Part { get; set; } = null!;

    public ICollection<Step> Steps { get; set; } = null!;
}