using Server.Modules.Core.Dtos;

namespace Shared.Dtos;

public class PartDto : TimestampDto
{
    public int Id;
    public string Name = null!;
    public string Opitz = null!;
    public ICollection<ProcedureDto> Procedures = null!;
}