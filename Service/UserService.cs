using AutoMapper;
using sdlt.Contracts;
using sdlt.DataTransferObjects;
using sdlt.Entities.Exceptions;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;
using sdlt.Service.Contracts;

namespace sdlt.Service;

internal sealed class UserService : IUserService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public UserService(IRepositoryManager repository, ILoggerManager
    logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<User>? users, MetaData metaData)> GetAllUsersAsync(UserParameters parameters, bool trackChanges)
    {
        PagedList<User> usersPaged = await _repository.User.GetAllUsers(parameters, trackChanges);
        // IEnumerable<UserDto> usersDto = _mapper.Map<IEnumerable<UserDto>>(usersPaged);
        // .Select(u => new UserDto() {
        //     Email = u.Email,
        //     FirstName = u.FirstName,
        //     LastName = u.LastName,
        //     PhoneNumber = u.PhoneNumber,
        //     UserName = u.UserName

        // });
        return (users: usersPaged, metaData: usersPaged.MetaData);
    }

    public async Task<UserDto> GetUserAsync(string id, bool trackChanges)
    {
        User user = await _repository.User.GetUser(id, trackChanges) ??
            throw new UserNotFoundException(id);
        return _mapper.Map<UserDto>(user);
        
    }
}
