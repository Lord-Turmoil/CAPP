using Shared.Dtos;

namespace Server.Modules.Core.Dtos;

public class ProcedureDto : TimestampDto
{
    public int Id;
    public string Description = null!;
    public string Order = null!;
    public ICollection<StepDto> Steps = null!;
}