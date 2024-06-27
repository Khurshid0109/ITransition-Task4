using Management.Service.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Management.Service.DTOs;
public record LoginPostModel
{
    [ManagementEmailAttribute]
    public string Email { get; set; }
    [MinLength(6),MaxLength(15)]
    public string Password { get; set; }
}
