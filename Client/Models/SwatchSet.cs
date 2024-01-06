namespace Client.Models;

class SwatchSet
{
    public string Name { get; set; } = null!;
    public IEnumerable<SwatchItem> Items { get; set; } = null!;
}