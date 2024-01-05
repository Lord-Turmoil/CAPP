using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Modules.Core.Models;

public class Step : TimestampModel
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
    public int ProcedureId { get; set; }

    [ForeignKey("ProcedureId")]
    public Procedure Procedure { get; set; } = null!;
}