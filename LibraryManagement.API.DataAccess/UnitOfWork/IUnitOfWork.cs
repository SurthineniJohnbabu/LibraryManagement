using System;
using System.Data.Common;

namespace LibraryManagement.API.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        DbCommand CreateCommand();
    }
}
