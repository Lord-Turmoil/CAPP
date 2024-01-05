namespace Server.Modules.Core.Dtos;

public class CreateStepDto
{
    public int ProcedureId { get; set; }
    public string Description { get; set; } = null!;
    public int Order { get; set; }
}