using GlowingNews.IdentityServer.Extensions;

namespace GlowingNews.IdentityServer.Context
{
    public static class MigrateDatabase
    {
        public static IHost MigrateDatabaseServices(this IHost host)
        {
            
            host.MigrateDatabase<IdentityServerContext>((context, services) =>
            {
                IdentityServerContextSeed
                    .SeedAsync(context)
                    .Wait();
            });

            return host;
        }
    }
}
