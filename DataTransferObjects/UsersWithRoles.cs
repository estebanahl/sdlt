
using CloudinaryDotNet.Actions;

namespace sdlt.DataTransferObjects;

public record class UserWithRoles : UserDto
{
    public IList<string> Roles {get;set;} = null!;
}
