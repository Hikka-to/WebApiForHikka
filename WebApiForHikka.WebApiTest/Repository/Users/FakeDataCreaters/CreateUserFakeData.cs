using Microsoft.AspNetCore.Identity;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.WebApiTest.Repository.Users.FakeDataCreaters;
public static class CreateUserFakeData
{
    public static async Task<List<Guid>> CreateUsersWithRoleAsync(HikkaDbContext databaseContext, UserManager<User> userManager, string role, uint howManyCreate)
    {
        List<Guid> ids = [];
        for (int i = 0; i < howManyCreate; ++i)
        {
            ids.Add(Guid.NewGuid());
            var user = databaseContext.Users.Add(
                new()
                {
                    Email = $"test{i + role}@gmail.com",
                    Id = ids[i],
                    PasswordHash = i.ToString(),
                    Role="Role",
                }
                );
            await databaseContext.SaveChangesAsync();
            await userManager.AddToRoleAsync(user.Entity, role);
        }

        return ids;
    }
}