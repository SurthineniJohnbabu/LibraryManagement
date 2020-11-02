using System.Threading.Tasks;

namespace LibraryManagement.API.DataAccess.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        Task<IUnitOfWork> Create();
    }
}
