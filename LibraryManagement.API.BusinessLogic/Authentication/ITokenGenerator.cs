using LibraryManagement.API.Core.Models;

namespace LibraryManagement.API.BusinessLogic.Authentication
{
    public interface ITokenGenerator
    {
        string GenerateToken(User userDetails);
    }
}
