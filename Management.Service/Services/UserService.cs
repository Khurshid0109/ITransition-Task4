using AutoMapper;
using Management.Service.DTOs;
using Management.Domain.Entities;
using Management.Service.Helpers;
using Management.Data.IRepositories;
using Management.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Management.Service.Helpers.Hasher;
using Management.Domain.Enums;

namespace Management.Service.Services;
public class UserService : IUserService
{
    private readonly IRepository _repository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IMapper _mapper;

    public UserService(IRepository repository, IMapper mapper, IJwtTokenService jwtTokenService)
    {
        _repository = repository;
        _mapper = mapper;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginViewModel> AddAsync(UserPostModel dto)
    {
        var user = await _repository.SelectAll()
            .Where(u=>u.Email.ToLower() == dto.Email.ToLower())
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user is not null)
          throw new ManagementException(409,"User already exists with this email!");

        var mapped=_mapper.Map<User>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.Password= HashPasswordHelper.PasswordHasher(dto.Password);
        (mapped.RefreshToken, mapped.ExpireDate) = await _jwtTokenService.GenerateRefreshTokenAsync();

        var result = await _repository.InsertAsync(mapped);
        await _repository.SaveAsync();

        var userView = _mapper.Map<UserViewModel>(result);
        (string token, DateTime expireDate) = await _jwtTokenService.GenerateTokenAsync(userView);

        return new LoginViewModel
        {
            Token = token,
            AccessTokenExpireDate = expireDate,
        };
    }

    public async Task<bool> BlockUsersAsync(List<long> ids)
    {
       foreach(var id in ids)
        {
            var user = await _repository.SelectByIdAsync(id);
            if(user is not null && user.Status == Status.Active)
            {
                user.UpdatedAt= DateTime.UtcNow;
                user.Status = Status.Blocked;
                await _repository.UpdateAsync(user);
                await _repository.SaveAsync();
            }

        }
        return true;
    }

    public async Task<LoginViewModel> LoginAsync(LoginPostModel dto)
    {
        var user = await _repository.SelectAll()
            .Where(u => u.Email.ToLower() == dto.Email.ToLower())
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if(user is null)
            throw new ManagementException(404, "User is not found!");

        if (!HashPasswordHelper.IsEqual(dto.Password, user.Password))
            throw new ManagementException(400,"email or password is incorrect!");

        if (user.Status == Status.Blocked)
            throw new ManagementException(400, "You can not enter there!");

        user.LastLoginDate = DateTime.UtcNow;
        await _repository.UpdateAsync(user);
        await _repository.SaveAsync();

        var userView = _mapper.Map<UserViewModel>(user);
        (string token, DateTime expireDate) = await _jwtTokenService.GenerateTokenAsync(userView);

        return new LoginViewModel
        {
            Token = token,
            AccessTokenExpireDate = expireDate,
        };
    }

    public Task<UserViewModel> ModifyAsync(long id, UserPutModel dto)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveUsersAsync(List<long> ids)
    {
        foreach (var id in ids)
        {
            var user = await _repository.SelectByIdAsync(id);
            if (user is not null)
            {
                await _repository.DeleteAsync(id);
                await _repository.SaveAsync();
            }

        }

        return true;
    }

    public async Task<IEnumerable<UserViewModel>> RetrieveAllAsync()
    {
        var users=await _repository.SelectAll()
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserViewModel>>(users);
    }

    public async Task<UserViewModel> RetrieveByEmailAsync(string email)
    {
        var user = await _repository.SelectAll()
           .Where(u => u.Email.ToLower() == email.ToLower())
           .AsNoTracking()
           .FirstOrDefaultAsync();

        if(user is null)
            throw new ManagementException(404, "User is not found!");

        return _mapper.Map<UserViewModel>(user);
    }

    public async Task<UserViewModel> RetrieveByIdAsync(long id)
    {
        var user = await _repository.SelectAll()
             .Where(u => u.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (user is null)
            throw new ManagementException(404, "User is not found!");

        return _mapper.Map<UserViewModel>(user);
    }

    public async Task<bool> UnBlockUsersAsync(List<long> ids)
    {
        foreach (var id in ids)
        {
            var user = await _repository.SelectByIdAsync(id);
            if (user is not null && user.Status == Status.Blocked)
            {
                user.UpdatedAt = DateTime.UtcNow;
                user.Status = Status.Active;
                await _repository.UpdateAsync(user);
                await _repository.SaveAsync();
            }

        }
        return true;
    }
}
