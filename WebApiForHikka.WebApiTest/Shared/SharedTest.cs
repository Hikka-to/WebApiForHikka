using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.Test.Shared;
public class SharedTest
{
    protected CancellationToken CancellationToken => new();

    protected HikkaDbContext GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<HikkaDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).EnableSensitiveDataLogging().Options;
        var databaseContext = new HikkaDbContext(options);
        databaseContext.Database.EnsureCreated();

        return databaseContext;
    }

    protected UserManager<User> GetUserManager() => GetUserManager(GetDatabaseContext());

    protected UserManager<User> GetUserManager(HikkaDbContext databaseContext)
    {
        var userStore = new UserStore<User, IdentityRole<Guid>, HikkaDbContext, Guid>(databaseContext);

        var options = new Mock<IOptions<IdentityOptions>>();
        var idOptions = new IdentityOptions();

        idOptions.User.RequireUniqueEmail = true;
        idOptions.Password.RequireDigit = true;
        idOptions.Password.RequireLowercase = true;
        idOptions.Password.RequireUppercase = true;
        idOptions.Password.RequireNonAlphanumeric = true;
        idOptions.Password.RequiredLength = 8;

        options.Setup(o => o.Value).Returns(idOptions);
        var userValidators = new List<IUserValidator<User>>();
        var validator = new UserValidator<User>();
        userValidators.Add(validator);

        var passValidator = new PasswordValidator<User>();
        var pwdValidators = new List<IPasswordValidator<User>>
        {
            passValidator
        };

        var passwordHasher = new PasswordHasher<User>();
        var passwordValidators = new List<IPasswordValidator<User>>();
        var keyNormalizer = new UpperInvariantLookupNormalizer();
        var errors = new IdentityErrorDescriber();
        var logger = new Logger<UserManager<User>>(new LoggerFactory());
        var userManager = new UserManager<User>
        (
            userStore,
            options.Object,
            passwordHasher,
            userValidators,
            pwdValidators,
            keyNormalizer,
            errors,
            null!,
            logger
        );

        return userManager;
    }

    protected RoleManager<IdentityRole<Guid>> GetRoleManager() => GetRoleManager(GetDatabaseContext());

    protected RoleManager<IdentityRole<Guid>> GetRoleManager(HikkaDbContext databaseContext)
    {
        var roleStore = new RoleStore<IdentityRole<Guid>, HikkaDbContext, Guid>(databaseContext);

        var options = new Mock<IOptions<IdentityOptions>>();
        var idOptions = new IdentityOptions();

        idOptions.User.RequireUniqueEmail = true;
        idOptions.Password.RequireDigit = true;
        idOptions.Password.RequireLowercase = true;
        idOptions.Password.RequireUppercase = true;
        idOptions.Password.RequireNonAlphanumeric = true;
        idOptions.Password.RequiredLength = 8;

        options.Setup(o => o.Value).Returns(idOptions);
        var roleValidators = new List<IRoleValidator<IdentityRole<Guid>>>();
        var validator = new RoleValidator<IdentityRole<Guid>>();
        roleValidators.Add(validator);

        var roleManager = new RoleManager<IdentityRole<Guid>>
        (
            roleStore,
            roleValidators,
            new UpperInvariantLookupNormalizer(),
            new IdentityErrorDescriber(),
            null!
        );

        return roleManager;
    }
}