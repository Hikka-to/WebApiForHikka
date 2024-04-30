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
    public const string SimplePasswordErrorMessage = "please enter a password that contains at least 6 characters";
}
