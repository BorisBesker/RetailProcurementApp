using Infrastructure.Models;

namespace Infrastructure.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        public User GetUserByUsernameAndPassword(string username, string password);

        bool ExistsWithSameUserName(string username);
    }
}
