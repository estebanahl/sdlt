namespace sdlt.DataTransferObjects;

public record OneFieldToUpdate<T>
{
    public T TheFieldToUpdate{get;set;} = default(T)!;
}
