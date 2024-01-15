using System.ComponentModel.DataAnnotations;
using backEnd;
using Npgsql.Replication;
using sdlt.Extensions;

namespace sdlt.DataTransferObjects;

[DateValidation()]
public record EventForManipulationDto
{
    [Required]
    public DateOnly StartDate { get; set; }
    [Required]
    public DateOnly EndDate { get; set; }
    [MaxLength(100, ErrorMessage = "Maximum length for the description is 100 characters.")]
    public string Description { get; set; } = string.Empty;
    [Range(30, 100, ErrorMessage = "The quota has to be rentable (minimum 30), and in OUR restaurant (max capacity 100)")]
    public ushort Quota { get; set; }
    [Required]
    public Guid EventTypeId { get; set; }
}