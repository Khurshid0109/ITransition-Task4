using Management.Domain.Commons;
using Management.Domain.Enums;

namespace Management.Domain.Entities;
public class User:Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public Status Status { get; set; }
    public Role Role { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpireDate { get; set; }
}
