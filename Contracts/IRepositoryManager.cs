
namespace sdlt.Contracts;
public interface IRepositoryManager
{
    ICategoryRepository Category { get; }
    IProductRepository Product { get; }
    void Save();
}