namespace WebApiForHikka.Constants.Models.Users;
public class UserStringConstants
{

    //User
    public const string RoleName = "Name";

    public const string EmailName = "Email";

    public const string PasswordName = "Password";

    //RegExpression

    public const string EmailRegExpression = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    public const string SimplePasswordRegExpression = @"^.{6,}$";


    //Error messages
    public const string SimplePasswordErrorMessage = "Please enter a password that contains at least 6 characters";

    public const string UserIsAlreadyExistErrorMessage = "User with this email is already exist";

    public const string RoleDoesntExist = "This role doesn't exist";


    //Claims
    public const string EmailClaim = "Email";

    public const string RoleClaim = "Role";

    public const string IdClaim = "Id";

    //Roles
    //every time when you create a new role you need to add it to the UsersRolesList 
    //If you won't do it it won't be recognize like a role in role validation attribute
    public const string UserRole = "User";

    public const string AdminRole = "Admin";

    public const string BannedRole = "Banned";

    //List of roles

    public static readonly IReadOnlyCollection<string> UsersRolesList = [
        UserRole.ToLower(),
        AdminRole.ToLower(),
        BannedRole.ToLower(),
    ];

    //API answered

    public const string MessageUserRegistrated = "User created successfully";

    public const string MessageUserIsntRegistrated = "Something went wrong during registration";
}