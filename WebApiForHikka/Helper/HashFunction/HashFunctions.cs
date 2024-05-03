namespace WebApiForHikka.WebApi.Helper.HashFunction;
public class HashFunctions : IHashFunctions
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
   
}
