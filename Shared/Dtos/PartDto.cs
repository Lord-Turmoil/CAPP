namespace Server.Modules.Core.Dtos;

public class PartDto : TimestampDto
{
    public int Id;
    public string Name = null!;
    public string Opitz = null!;
    public ICollection<ProcedureDto> Procedures = null!;
}