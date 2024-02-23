using Management.Domain.Entities;
using Management.Data.DbContexts;
using Management.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Management.Data.Repositories;
public class Repository : IRepository
{
    protected readonly DataContext _context;

    public Repository(DataContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(e => e.Id.Equals(id));
        _context.Users.Remove(entity);

        return true;
    }

    public async Task<bool> DeleteMany(ICollection<long> ids)
    {
        // Get entities by their keys
        var entitiesToDelete = _context.Users.Where(entity => ids.Contains(entity.Id)).ToList();

        // If there are entities to delete, remove them
        if (entitiesToDelete.Any())
        {
            _context.Users.RemoveRange(entitiesToDelete);
            _context.SaveChanges();
        }

        return true;
    }

    public async Task<User> InsertAsync(User entity)
    {
        var entry = await _context.Users.AddAsync(entity);

        return entry.Entity;
    }

    public async Task SaveAsync()
    => await _context.SaveChangesAsync();

    public IQueryable<User> SelectAll()
     => _context.Users;

    public async Task<User> SelectByIdAsync(long id)
    => await _context.Users.FirstOrDefaultAsync(e => e.Id.Equals(id));

    public async Task<User> UpdateAsync(User entity)
    {
        var entry = _context.Update(entity);
        return entry.Entity;
    }
}
