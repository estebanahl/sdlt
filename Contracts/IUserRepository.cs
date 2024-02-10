using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;

namespace sdlt.Contracts;

public interface IUserRepository
{
    Task<PagedList<User>> GetAllUsers(UserParameters userParameters, bool trackChanges);
    Task<User?> GetUser(string guidOruserName, bool trackChanges);
    
}
