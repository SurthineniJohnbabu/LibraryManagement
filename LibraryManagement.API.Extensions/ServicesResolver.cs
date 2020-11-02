using LibraryManagement.API.DataAccess.Repositories.UserRepository;
using LibraryManagement.API.DataAccess.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using LibraryManagement.API.BusinessLogic.Books;
using LibraryManagement.API.DataAccess.Repositories.BooksRepository;
using LibraryManagement.API.BusinessLogic.Users;
using LibraryManagement.API.BusinessLogic.Authentication;

namespace LibraryManagement.API.Extensions
{
    public static class ServicesResolver
    {
        public static void ResolveDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Sql");
            var commandTimeout = configuration.GetValue("CommandTimeout", 720);

            services.AddSingleton<IUnitOfWorkFactory>(
                options => new UnitOfWorkFactory(connectionString, commandTimeout));

            services.AddTransient<IUnitOfWork, AdoNetUnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBooksRepository, BooksRepository>();
        }
        public static void ResolveBusinessLogicServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserServiceManager, UserServiceManager>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<IBooksManager, BooksManager>();
        }
    }
}
