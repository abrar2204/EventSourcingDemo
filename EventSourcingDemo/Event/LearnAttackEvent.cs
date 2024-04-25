namespace EventSourcingDemo.Event;

public class LearnAttackEvent : BaseEvent
{
    public LearnAttackEvent() : base(nameof(LearnAttackEvent))
    {
    }

    public string Attack { get; set; }
}