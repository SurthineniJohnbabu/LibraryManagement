using System.Data;
using System.Threading.Tasks;

namespace LibraryManagement.API.DataAccess.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<DataSet> GetAuthenticatedUserDetails(string userName, string password);
        Task<DataSet> GetAllUsers();
        Task<DataSet> GetUserById(int userId);
    }
}
