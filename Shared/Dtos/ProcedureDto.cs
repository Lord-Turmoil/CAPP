using Shared.Dtos;

namespace Server.Modules.Core.Dtos;

public class ProcedureDto : TimestampDto
{
    public int Id;
    public string Name = null!;
    public string Opitz = null!;
    public ICollection<StepDto> Steps = null!;
}