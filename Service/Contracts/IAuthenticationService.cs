using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using sdlt.DataTransferObjects;

namespace sdlt.Service.Contracts;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
    Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
    Task<IdentityResult> CreateRole(CreateRoleViewDto model);
    Task<string> CreateToken();
    Task<UserDto> GetMyself(IIdentity userIdentificator);
}