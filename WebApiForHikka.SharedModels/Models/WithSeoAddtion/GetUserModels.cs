using Faker;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.Users;

namespace WebApiForHikka.SharedModels.Models.WithSeoAddtion;

public class GetUserModels
{
    
    public static User GetSample()
    {
        return new User
        {
            Email = "test",
        };
    }
    
    public static User GetSampleForUpdate()
    {
        return new User
        {
            Email = "test1",
        };
    }
    
    public static UserRegistrationDto GetCreateDtoSample()
    {
        return new UserRegistrationDto()
        {
           Email = Lorem.GetFirstWord(),
           UserName = "test",
           Password = "test",
           Role = "test"
        };
    }
    
    public static GetUserDto GetGetDtoSample()
    {
        return new GetUserDto
        {
            Email = "test1",
            Roles = []
        };
    }
    
    public static UpdateUserDto GetUpdateDtoSample()
    {
        return new UpdateUserDto
        {
            Id = Guid.NewGuid(),
            Email = "test2",
            Role = "test2"
        };
    }
}