using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventSourcingDemo.Event;

namespace EventSourcingDemo.Entities;

[Table("EventStore")]
public class EventStore
{
    [Key]
    public string Id { get; set; }
    public Guid AggregateIdentifier { get; set; }
    public int Version { get; set; }
    public string EventType { get; set; }
    public string EventData { get; set; }
}