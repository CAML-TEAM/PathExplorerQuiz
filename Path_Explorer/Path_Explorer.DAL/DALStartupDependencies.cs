using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Path_Explorer.DAL;
using Path_Explorer.DAL.Context;

namespace Path_Explorer.DAL
{
    public static class DALStartupDependencies
    {
        public static IServiceCollection AddDALApplicationDependencies(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddDbContext<PathExplorerDbContext>(options =>
                       options.UseLazyLoadingProxies().UseNpgsql(Configuration.GetConnectionString(DALConstants.CoreConnectionName)), ServiceLifetime.Scoped);
            
            services.AddDbContext<PathExplorerMssqlDbContext>(options =>
                       options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString(DALConstants.MssqlCoreConnectionName)), ServiceLifetime.Scoped);
            
            services.AddDbContext<PathExplorerSqlLiteDbContext>(options =>
                       options.UseLazyLoadingProxies().UseSqlite(Configuration.GetConnectionString(DALConstants.SqlLiteCoreConnectionName)), ServiceLifetime.Scoped);


            return services;
        }
    }
}
