using Infrastructure.Data;
using Infrastructure.Models;

namespace Infrastructure.Repository
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        public UserRepository(RetailProcurementContext context)
            : base(context) { }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return Context.Set<User>().Where(x => x.UserName == username && x.Password == password).SingleOrDefault();
        }

        public bool ExistsWithSameUserName(string username)
        {
            return Context.Set<User>().Any(x => x.UserName.Trim().ToUpper() == username.Trim());
        }
    }
}
