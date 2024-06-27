using Management.Service.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Management.Service.DTOs;
public record UserPostModel
{
    [MinLength(3),MaxLength(10)]
    public string FirstName { get; set; }
    [MinLength(3),MaxLength(10)]
    public string LastName { get; set; }

    [ManagementEmailAttribute]
    public string Email { get; set; }
    [MinLength(6),MaxLength(15)]
    public string Password { get; set; }
}
