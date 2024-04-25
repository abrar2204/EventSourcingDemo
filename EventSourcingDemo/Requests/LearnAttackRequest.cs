namespace EventSourcingDemo.Requests;

public class LearnAttackRequest
{
    public Guid Id { get; set; }
    public string Attack { get; set; }
}