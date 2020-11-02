using LibraryManagement.API.DataAccess.UnitOfWork;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LibraryManagement.API.DataAccess.Repositories.UserRepository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
        }

        public async Task<DataSet> GetAllUsers()
        {
            var parameters = new SqlParameter[] 
            {
                new SqlParameter(Constants.Parameters.USER_ID, DBNull.Value)
            };

            try
            {
                return await base.ReadFromStoredProcedure(Constants.Procedures.LM_GET_USER_DETAILS, parameters);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<DataSet> GetUserById(int userId)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter(Constants.Parameters.USER_ID, userId)
            };

            try
            {
                return await base.ReadFromStoredProcedure(Constants.Procedures.LM_GET_USER_DETAILS, parameters);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<DataSet> GetAuthenticatedUserDetails(string userName, string password)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter(Constants.Parameters.USER_NAME, userName),
                new SqlParameter(Constants.Parameters.PASSWORD, password)
            };

            try
            {
                return await base.ReadFromStoredProcedure(Constants.Procedures.LM_AUTHENTICATE_USER_DETAILS, parameters);
            }
            catch (Exception ex)
            { throw ex; }
        }

        private static class Constants
        {
            public static class Parameters
            {
                public const string USER_NAME = "@UserName";
                public const string USER_ID = "@UserId";
                public const string PASSWORD = "@Password";
            }
            public static class Procedures
            {
                public const string LM_AUTHENTICATE_USER_DETAILS = "LM_AuthenticateUserDetails";
                public const string LM_GET_USER_DETAILS = "LM_GetUserDetails";
            }
        }
    }
}
