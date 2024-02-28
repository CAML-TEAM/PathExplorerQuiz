
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Path_Explorer.DAL.Context;

public sealed class DbCoreInitializer
{
    public static void Initialize(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<PathExplorerMssqlDbContext>();

        context.Database.EnsureCreated();
        context.Database.Migrate();

    }
}
