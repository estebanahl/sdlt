using sdlt.Entities.Models;

namespace sdlt.Contracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
        Task<Category?> GetCategoryAsync(Guid categoryId, bool trackChanges);
    }
}