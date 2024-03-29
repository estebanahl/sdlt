using sdlt.Service.Contracts;

public interface IServiceManager
{
    IProductService ProductService { get; }
    ICategoryService CategoryService { get; }
    IAuthenticationService AuthenticationService { get; }
    IEventService EventService { get; }
    IBookingService BookingService { get; }
    IUserService UserService { get; }
}