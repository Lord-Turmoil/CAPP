using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Server.Modules.Core.Dtos;

public class StepDto : TimestampDto
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public int Order { get; set; }
}