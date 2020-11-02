using LibraryManagement.API.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.API.BusinessLogic.Users
{
    public interface IUserServiceManager
    {
        Task<User> Authenticate(string username, string password);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int userId);
    }
}
