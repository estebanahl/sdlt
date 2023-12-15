

using AutoMapper;
using sdlt.Contracts;
using sdlt.DataTransferObjects;
using sdlt.Service.Contracts;
namespace sdlt.Service;
internal sealed class CategoryService : ICategoryService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public CategoryService(IRepositoryManager repository, ILoggerManager
    logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public IEnumerable<CategoryDto> GetAllCategories(bool trackChanges)
    {
        try{
            var categories = _repository.Category.GetAllCategories(trackChanges);
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoriesDto;
        }catch(Exception ex){
            _logger.LogError($"Something went VERY wrong in the {nameof(GetAllCategories)} service method: {ex}");
            throw;
        }
    }
}
