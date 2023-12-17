namespace sdlt.DataTransferObjects;

public record ProductForCreationDto(string Name, string Description, Guid CategoryId, IFormFile Image);