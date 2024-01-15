using System.ComponentModel.DataAnnotations.Schema;

namespace sdlt.Entities.Models;

[Table("event_type")]
public class EventType // 
{
    [Column("event_type_id")]
    public Guid Id { get; set; }
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    [Column("description")]
    public string Description { get; set; } = string.Empty;
    [Column("active")]
    public bool Active {get;set;}
    [Column("default_quota")]
    public byte DefaultQuota { get; set; }
    public virtual IEnumerable<Event> Events { get; set; } = new List<Event>();
}