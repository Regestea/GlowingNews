using GlowingNews.IdentityServer.Entities;
using GlowingNews.IdentityServer.Extensions;
using Microsoft.EntityFrameworkCore;
using Security.Shared;

namespace GlowingNews.IdentityServer.Context
{
    public class IdentityServerContextSeed
    {
        public static async Task SeedAsync(IdentityServerContext accountContext)
        {
            if (!accountContext.Roles.Any())
            {
                accountContext.Roles.Add(new Role() { Id = Guid.NewGuid(), RoleTitle = Roles.User });
                accountContext.Roles.Add(new Role() { Id = Guid.NewGuid(), RoleTitle = Roles.Admin });

                await accountContext.SaveChangesAsync();
            }

            if (!accountContext.Users.Any())
            {
                accountContext.Users.Add(new User()
                {
                    Id = Guid.NewGuid(), Email = "admin@gmail.com", Name = "Mr Admin",
                    Password = PasswordHash.Hash("Admin1234")
                });

                await accountContext.SaveChangesAsync();
            }

            if (!accountContext.UserRoles.Any())
            {
                var admin = await accountContext.Users.Where(x => x.Email == "admin@gmail.com" && x.Name == "Mr Admin")
                    .SingleAsync();
                var roles = await accountContext.Roles.ToListAsync();
                foreach (var role in roles)
                {
                    await accountContext.UserRoles.AddAsync(new UserRole()
                        { Id = Guid.NewGuid(), RoleId = role.Id, UserId = admin.Id });
                }

                await accountContext.SaveChangesAsync();
            }
        }
    }
}