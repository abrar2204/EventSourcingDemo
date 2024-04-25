namespace EventSourcingDemo.Requests;

public class EvolvePokemonRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public List<string> Type { get; set; }
}