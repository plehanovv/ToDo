using Microsoft.EntityFrameworkCore.Storage;
using ToDo.Domain.Entity;
using ToDo.Domain.Interfaces.Databases;
using ToDo.Domain.Interfaces.Repositories;

namespace ToDo.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context, IBaseRepository<User> users, 
        IBaseRepository<Role> roles, IBaseRepository<UserRole> userRoles)
    {
        _context = context;
        Users = users;
        Roles = roles;
        UserRoles = userRoles;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public IBaseRepository<User> Users { get; set; }
    
    public IBaseRepository<Role> Roles { get; set; }
    
    public IBaseRepository<UserRole> UserRoles { get; set; }
}