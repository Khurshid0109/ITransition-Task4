using Management.Service.Helpers;

namespace Management.Service.DTOs;
public record LoginPostModel
{
    [ManagementEmailAttribute]
    public string Email { get; set; }
    public string Password { get; set; }
}
