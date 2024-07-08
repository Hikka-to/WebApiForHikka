namespace WebApiForHikka.SharedFunction.HashFunction;

public interface IHashFunctions
{
    public string HashPassword(string password);
    public bool VerifyPassword(string enteredPassword, string storedHash);
}