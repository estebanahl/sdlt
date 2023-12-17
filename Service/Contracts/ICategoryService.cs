using sdlt.DataTransferObjects;

namespace sdlt.Service.Contracts;
public interface ICategoryService{
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges);
    Task<CategoryDto> GetCategoryAsync(Guid categoryId, bool trackChanges);
}