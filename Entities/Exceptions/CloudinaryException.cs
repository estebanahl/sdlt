
namespace sdlt.Entities.Exceptions;

public sealed class CloudinaryException : NotFoundException
{
    public CloudinaryException(string message) :
        base($"There was an error trying to save the photo with message: {message}")
    { }
}