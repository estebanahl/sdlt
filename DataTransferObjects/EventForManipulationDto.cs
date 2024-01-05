using System.ComponentModel.DataAnnotations;

namespace sdlt.DataTransferObjects;

public record EventForManipulationDto
{
    [Required]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string Name { get; set; } = string.Empty;
    public DateOnly EventDate { get; set; }
    [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters.")]
    public string Description { get; set; } = string.Empty;
    public ushort Quota { get; set; }
}