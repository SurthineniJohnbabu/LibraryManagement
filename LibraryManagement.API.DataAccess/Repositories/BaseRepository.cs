using LibraryManagement.API.DataAccess.UnitOfWork;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LibraryManagement.API.DataAccess.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly IUnitOfWorkFactory unitOfWorkFactory;

        protected BaseRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        protected virtual async Task<DataSet> ReadFromStoredProcedure(string procedureName, SqlParameter[] parameters = null)
        {
            DataSet dataSet = new DataSet();

            using (var uow = await this.unitOfWorkFactory.Create())
            using (var command = uow.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procedureName;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                var dataAdapter = new SqlDataAdapter((SqlCommand)command);
                dataAdapter.Fill(dataSet);
            }

            return dataSet;
        }

        protected virtual async Task<int> ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
        {
            using (var uow = await this.unitOfWorkFactory.Create())
            using (var command = uow.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procedureName;
                command.Parameters.AddRange(parameters);

                var rowCount = await command.ExecuteNonQueryAsync();

                return rowCount;
            }
        }
    }
}
