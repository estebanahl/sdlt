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
    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager
        logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
        _categoryService = new Lazy<ICategoryService>(() => new
            CategoryService(repositoryManager, logger, mapper));
        _productService = new Lazy<IProductService>(() => new
            ProductService(repositoryManager, logger, mapper, configuration));
        _authenticationService = new Lazy<IAuthenticationService>(() => new
            AuthenticationService(logger, mapper, userManager, configuration));
    }
    public ICategoryService CategoryService => _categoryService.Value;
    public IProductService ProductService => _productService.Value;
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
}