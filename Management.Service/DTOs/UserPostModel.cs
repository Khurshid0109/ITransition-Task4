using Management.Service.Helpers;

namespace Management.Service.DTOs;
public record UserPostModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [ManagementEmailAttribute]
    public string Email { get; set; }
    public string Password { get; set; }
}
