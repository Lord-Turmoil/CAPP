using Server.Modules.Core.Dtos;

namespace Shared.Dtos;

public class GroupDto : TimestampDto
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public string Matrix { get; set; } = null!;

    public ICollection<ProcedureDto> Procedures { get; set; } = null!;
}