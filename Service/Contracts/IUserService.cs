using backEnd;
using sdlt.DataTransferObjects;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;

namespace sdlt.Service.Contracts;

public interface IUserService
{
    Task<(IEnumerable<User>? users, MetaData metaData)> GetAllUsersAsync(
        UserParameters parameters, bool trackChanges);
    Task<UserDto> GetUserAsync(string id, bool trackChanges);
}
