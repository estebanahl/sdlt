

using AutoMapper;
using sdlt.Contracts;
using sdlt.DataTransferObjects;
using sdlt.Entities.Exceptions;
using sdlt.Entities.Models;
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

    public async Task<CategoryDto> CreateCategory(CategoryForCreationDto categoryForCreation, bool trackChanges)
    {
        var categoryEntity = _mapper.Map<Category>(categoryForCreation);
        _repository.Category.CreateCategory(categoryEntity);
        await _repository.SaveAsync();

        var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);

        return categoryToReturn;
    }

    public async Task DeleteCategory(Guid categoryId, bool trackChanges)
    {
        var category = await _repository.Category.GetCategoryAsync(categoryId, trackChanges)
            ?? throw new CategoryNotFoundException(categoryId);
        _repository.Category.DeleteCategory(category);
        await _repository.SaveAsync();
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges)
    {
        // try{
            var categories = await _repository.Category.GetAllCategoriesAsync(trackChanges);
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoriesDto;
        // }catch(Exception ex){
        //     _logger.LogError($"Something went VERY wrong in the {nameof(GetAllCategories)} service method: {ex}");
        //     throw;
        // }
    }

    public async Task<CategoryDto> GetCategoryAsync(Guid id, bool trackChanges)
    {
        var category = await _repository.Category.GetCategoryAsync(id, trackChanges);

        var categoryDto = _mapper.Map<CategoryDto>(category);

        return categoryDto;
    }
}
