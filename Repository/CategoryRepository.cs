using Microsoft.EntityFrameworkCore;
using sdlt.Contracts;
using sdlt.Entities.Models;

namespace sdlt.Repository;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {
    }

    public void CreateCategory(Category category) => Create(category);

    public void DeleteCategory(Category category) => Delete(category);

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges) =>
        await FindAll(trackChanges)
            // .OrderBy(c => c)
            .ToListAsync();

    public async Task<Category?> GetCategoryAsync(Guid categoryId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(categoryId), trackChanges)
            .SingleOrDefaultAsync();
}