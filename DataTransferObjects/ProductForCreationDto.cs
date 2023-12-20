using System.ComponentModel.DataAnnotations;

namespace sdlt.DataTransferObjects;

public record ProductForCreationDto  : ProductForManipulationDto
{
    public IFormFile Image { get; init; } = null!;
}