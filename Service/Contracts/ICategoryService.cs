using sdlt.DataTransferObjects;

namespace sdlt.Service.Contracts;
public interface ICategoryService{
    IEnumerable<CategoryDto> GetAllCategories(bool trackChanges);
}