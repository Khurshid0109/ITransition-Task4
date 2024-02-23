using Management.Domain.Enums;

namespace Management.Service.DTOs;
public record UserViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public Status Status { get; set; }
}
