

using AutoMapper;
using sdlt.Contracts;
using sdlt.Service.Contracts;
namespace sdlt.Service;
internal sealed class ProductService : IProductService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    public readonly IMapper _mapper;
    public ProductService(IRepositoryManager repository, ILoggerManager
    logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
}
