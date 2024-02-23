namespace Management.Service.DTOs;
public record LoginPostModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
