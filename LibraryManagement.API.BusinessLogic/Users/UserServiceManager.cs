using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.API.BusinessLogic.Authentication;
using LibraryManagement.API.Core.Models;
using LibraryManagement.API.DataAccess.Repositories.UserRepository;

namespace LibraryManagement.API.BusinessLogic.Users
{
    public class UserServiceManager : IUserServiceManager
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenGenerator tokenGenerator;

        public UserServiceManager(IUserRepository userRepository, ITokenGenerator tokenGenerator)
        {
            this.userRepository = userRepository;
            this.tokenGenerator = tokenGenerator;
        }
        public async Task<User> Authenticate(string username, string password)
        {
            var userTable = await userRepository.GetAuthenticatedUserDetails(username, password);
            var user = MapAuthenticatedUserDetails(userTable.Tables[0]);

            if (user == null)
                return null;

            user.Token = tokenGenerator.GenerateToken(user);

            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var usersTable = await userRepository.GetAllUsers();
            var users = MapUsers(usersTable.Tables[0]);

            return users;
        }

        private List<User> MapUsers(DataTable usersTable)
        {
            return usersTable.AsEnumerable().Select(s => new User
            {
                UserId = s.Field<int>("UserId"),
                UserName = s.Field<string>("UserName"),
                FirstName = s.Field<string>("FirstName"),
                LastName = s.Field<string>("LastName"),
                Role = s.Field<string>("RoleName")
            }).ToList();
        }

        public async Task<User> GetUserById(int userId)
        {
            var usersTable = await userRepository.GetAllUsers();
            var user = MapUser(usersTable.Tables[0]);

            return user;
        }

        private User MapUser(DataTable userTable)
        {
            return userTable.AsEnumerable().Select(s => new User
            {
                UserId = s.Field<int>("UserId"),
                UserName = s.Field<string>("UserName"),
                FirstName = s.Field<string>("FirstName"),
                LastName = s.Field<string>("LastName"),
                Role = s.Field<string>("RoleName")
            }).FirstOrDefault();
        }

        private User MapAuthenticatedUserDetails(DataTable userTable)
        {
            return userTable.AsEnumerable().Select(s => new User
            {
                UserId = s.Field<int>("UserId"),
                UserName = s.Field<string>("UserName"),
                FirstName = s.Field<string>("FirstName"),
                LastName = s.Field<string>("LastName"),
                Role = s.Field<string>("RoleName")
            }).FirstOrDefault();
        }
    }
}
