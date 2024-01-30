using Infrastructure.Models;
using ServiceLayer.Models;

namespace ServiceLayer.Services
{
    public interface IUserService
    {
        string Login(User user);
        ServiceResponse<User> Reigster(User user);
    }
}
