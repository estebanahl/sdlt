using System.ComponentModel.DataAnnotations;

namespace sdlt.DataTransferObjects;

public record EventForManipulationDto
{
    [Required]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string Name { get; set; } = string.Empty;
    [Required]
    public DateOnly EventDate { get; set; }
    [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters.")]
    [Required]
    public string Description { get; set; } = string.Empty;
    [Range(30, 100, ErrorMessage = "The quota has to be rentable (minimum 30), and in OUR restaurant (max capacity 100)")]
    public ushort Quota { get; set; }
}