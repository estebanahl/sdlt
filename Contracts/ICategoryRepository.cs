using sdlt.Entities.Models;

namespace sdlt.Contracts
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories(bool trackChanges);
    }
}