namespace WebApiForHikka.Constants.Users;
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


    //Claims
    public const string EmailClaim = "Email";

    public const string RoleClaim = "Role";

    public const string IdClaim = "Id";

    //Roles
    public const string UserRole = "User";

    public const string AdminRole = "Admin";

    public const string BannedRole = "Banned";
}
