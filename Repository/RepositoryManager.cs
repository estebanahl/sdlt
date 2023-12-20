using sdlt.Contracts;

namespace sdlt.Repository;
public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<ICategoryRepository> _categoryRepository;
    private readonly Lazy<IProductRepository> _productRepository;
    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _categoryRepository = new Lazy<ICategoryRepository>(() => new
        CategoryRepository(repositoryContext));
        _productRepository = new Lazy<IProductRepository>(() => new
        ProductRepository(repositoryContext));
    }
    public ICategoryRepository Category => _categoryRepository.Value;
    public IProductRepository Product => _productRepository.Value;
    
    public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
}