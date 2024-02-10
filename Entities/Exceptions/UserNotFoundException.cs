namespace sdlt.Entities.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string identificator)
        : base($"User with identificator {identificator} has not been found in database!") { }
}
