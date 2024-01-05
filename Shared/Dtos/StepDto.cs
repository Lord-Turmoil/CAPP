namespace Shared.Dtos;

public class StepDto : TimestampDto
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public int Order { get; set; }
}