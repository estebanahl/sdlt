using AutoMapper;
using Microsoft.AspNetCore.Identity;
using sdlt.Contracts;
using sdlt.Entities.Models;
using sdlt.Service.Contracts;

namespace sdlt.Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<ICategoryService> _categoryService;
    private readonly Lazy<IProductService> _productService;
    private readonly Lazy<IAuthenticationService> _authenticationService;
    private readonly Lazy<IEventService> _eventService;
    private readonly Lazy<IBookingService> _bookingService;
    private readonly Lazy<IUserService> _userService;
    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager
        logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration
        , RoleManager<IdentityRole> roleManager)
    {
        _categoryService = new Lazy<ICategoryService>(() => new
            CategoryService(repositoryManager, logger, mapper));
        _productService = new Lazy<IProductService>(() => new
            ProductService(repositoryManager, logger, mapper, configuration));
        _authenticationService = new Lazy<IAuthenticationService>(() => new
            AuthenticationService(logger, mapper, userManager, configuration, roleManager));
        _eventService = new Lazy<IEventService>(() => new
            EventService(repositoryManager, logger, mapper));
        _bookingService = new Lazy<IBookingService>(() => new
            BookingService(repositoryManager, logger, mapper, userManager));
        _userService = new Lazy<IUserService>(() => new
            UserService(repositoryManager, logger, mapper));
    }
    public ICategoryService CategoryService => _categoryService.Value;
    public IProductService ProductService => _productService.Value;
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
    public IEventService EventService => _eventService.Value;
    public IBookingService BookingService => _bookingService.Value;
    public IUserService UserService => _userService.Value;
}