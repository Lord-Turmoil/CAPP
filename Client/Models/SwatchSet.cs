namespace Client.Models;

class SwatchSet
{
    public string Name { get; set; }
    public IEnumerable<SwatchItem> Items { get; set; }
}