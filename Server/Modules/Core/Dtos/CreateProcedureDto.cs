namespace Server.Modules.Core.Dtos;

public class CreateProcedureDto
{
    public int PartId { get; set; }
    public string Description { get; set; } = null!;
    public int Order { get; set; }
}