namespace sdlt.DataTransferObjects;

public record ProductDto(Guid Id, string Name, string Description, string Category, decimal Price, string ImageUrl);