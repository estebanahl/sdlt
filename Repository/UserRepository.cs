using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using sdlt.Contracts;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;
using sdlt.Repository;
using sdlt.Repository.Extensions;

namespace backEnd;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(RepositoryContext repositoryContext) 
        : base (repositoryContext){}
    public async Task<PagedList<User>> GetAllUsers(UserParameters userParameters, bool trackChanges)
    {
        var query = FindAll(trackChanges)
             .Sort(userParameters.OrderBy!)
             .Skip((userParameters.PageNumber - 1) * userParameters.PageSize)
             .Take(userParameters.PageSize);

        int count = await query.CountAsync();
        var users = await query.ToListAsync();
        
        return PagedList<User>.ToPagedList(users, count, userParameters.PageNumber, userParameters.PageSize);
    }

    public async Task<User?> GetUser(string guidOruserName,  bool trackChanges)
    {
        if(Guid.TryParse(guidOruserName, out Guid theGuid))
            return await FindByCondition(u => u.Id.Equals(theGuid), trackChanges).SingleOrDefaultAsync();
        else
            return await FindByCondition(u => u.UserName.Equals(guidOruserName), trackChanges).SingleOrDefaultAsync();
        
    }
}
