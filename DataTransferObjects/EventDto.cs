namespace sdlt.DataTransferObjects;

public record EventDto (Guid Id, string Name, DateOnly EventDate, string Description, ushort Quota, bool Active);