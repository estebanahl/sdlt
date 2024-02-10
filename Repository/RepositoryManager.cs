using backEnd;
using sdlt.Contracts;

namespace sdlt.Repository;
public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<ICategoryRepository> _categoryRepository;
    private readonly Lazy<IProductRepository> _productRepository;
    private readonly Lazy<IEventRepository> _eventRepository;
    private readonly Lazy<IBookingRepository> _bookingRepository;
    private readonly Lazy<IUserRepository> _userRepository;
    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _categoryRepository = new Lazy<ICategoryRepository>(() => new
        CategoryRepository(repositoryContext));
        _productRepository = new Lazy<IProductRepository>(() => new
        ProductRepository(repositoryContext));
        _eventRepository = new Lazy<IEventRepository>(() => new
        EventRepository(repositoryContext));
        _bookingRepository = new Lazy<IBookingRepository>(() => new
        BookingRepository(repositoryContext));
        _userRepository = new Lazy<IUserRepository>(() => new
        UserRepository(repositoryContext));
    }
    public ICategoryRepository Category => _categoryRepository.Value;
    public IProductRepository Product => _productRepository.Value;
    public IEventRepository Event => _eventRepository.Value;
    public IBookingRepository Booking => _bookingRepository.Value;
    public IUserRepository User => _userRepository.Value;
    
    public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
}