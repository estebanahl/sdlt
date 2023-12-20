using System.ComponentModel.DataAnnotations;

namespace sdlt.DataTransferObjects;

public record ProductForManipulationDto
{
    [Required(ErrorMessage = "The Product name is a required field.")]
    [MaxLength(55, ErrorMessage = "Maximum length for the Name is 55 characters")]
    public string Name { get; init; } = string.Empty;
    [MaxLength(110, ErrorMessage = "Maximum length for the Name is 55 characters")]
    public string Description { get; init; } = string.Empty;
    [Required(ErrorMessage = "The Product must have a category id.")]
    public Guid CategoryId { get; init; }
}