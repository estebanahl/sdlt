
namespace sdlt.Contracts;
public interface IRepositoryManager
{
    ICategoryRepository Category { get; }
    IProductRepository Product { get; }
    IEventRepository Event { get; }
    Task SaveAsync();
}