using Management.Service.DTOs;

namespace Management.Service.Interfaces;
public interface IUserService
{
    Task<bool> BlockUsersAsync(List<long> id);
    Task<bool> UnBlockUsersAsync(List<long> id);
    Task<bool> RemoveUsersAsync(List<long> id);
    Task<bool> LoginAsync(LoginPostModel dto);
    Task<UserViewModel> RetrieveByIdAsync(long id);
    Task<IEnumerable<UserViewModel>> RetrieveAllAsync();
    Task<UserViewModel> AddAsync(UserPostModel dto);
    Task<UserViewModel> RetrieveByEmailAsync(string email);
    Task<UserViewModel> ModifyAsync(long id, UserPutModel dto);
}
