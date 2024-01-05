using Server.Modules.Core.Dtos;

namespace Shared.Dtos;

public class PartDto : TimestampDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Opitz { get; set; } = null!;
    public ICollection<ProcedureDto> Procedures { get; set; } = null!;
}