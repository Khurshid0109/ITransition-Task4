using Management.Domain.Entities;

namespace Management.Data.IRepositories;
public interface IRepository
{
    Task<User> InsertAsync(User entity);
    Task<User> UpdateAsync(User entity);
    IQueryable<User> SelectAll();
    Task<User> SelectByIdAsync(long id);
    Task<bool> DeleteAsync(long id);
    Task<bool> DeleteMany(ICollection<long> id);
    Task SaveAsync();
}
