namespace EventSourcingDemo.Requests;

public class CapturePokemonRequest
{
    public string Name { get; set; }
    public int Level { get; set; }
    public List<string> Type { get; set; }
    public List<string> Attacks { get; set; } 
}