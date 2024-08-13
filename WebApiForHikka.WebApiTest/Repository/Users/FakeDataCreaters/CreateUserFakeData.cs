using Microsoft.AspNetCore.Identity;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Repository.Users.FakeDataCreaters;

public static class CreateUserFakeData
{
    public static async Task<List<Guid>> CreateUsersWithRoleAsync(HikkaDbContext databaseContext,
        RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager, string role, uint howManyCreate)
    {
        List<Guid> ids = [];
        for (var i = 0; i < howManyCreate; ++i)
        {
            ids.Add(Guid.NewGuid());

            var user = databaseContext.Users.Add(GetUserModels.GetSample());

            await databaseContext.SaveChangesAsync();
            await userManager.AddToRoleAsync(user.Entity, role);
        }

        return ids;
    }
}