using Shared.Dtos;

namespace Server.Modules.Core.Dtos;

public class ProcedureDto : TimestampDto
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public string Order { get; set; } = null!;
}