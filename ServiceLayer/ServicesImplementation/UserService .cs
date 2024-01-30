using Infrastructure.Models;
using Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Models;
using ServiceLayer.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceLayer.ServicesImplementation
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _database;

        public UserService(IConfiguration configuration, IUnitOfWork database) 
        {
            _configuration = configuration;
            _database = database;
        }

        public string Login(User user)
        {
            var LoginUser = _database.Users.GetUserByUsernameAndPassword(user.UserName, user.Password);

            if (LoginUser == null)
            {
                return string.Empty;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string userToken = tokenHandler.WriteToken(token);

            return userToken;
        }


        public ServiceResponse<User> Reigster(User user)
        {
            if(_database.Users.ExistsWithSameUserName(user.UserName))
            {
                return new ServiceResponse<User> { Success = false, UserNameExists = true };
            }

            _database.Users.Add(user);

            _database.Save();

            return new ServiceResponse<User> { Success = true, UserNameExists = false };
        }
    }
}
