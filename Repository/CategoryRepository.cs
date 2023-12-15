using sdlt.Contracts;
using sdlt.Entities.Models;
using sdlt.Repository;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {
    }

    public IEnumerable<Category> GetAllCategories(bool trackChanges) =>
        FindAll(trackChanges)
            // .OrderBy(c => c)
            .ToList();
}