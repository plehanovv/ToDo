using Microsoft.EntityFrameworkCore.Storage;
using ToDo.Domain.Entity;
using ToDo.Domain.Interfaces.Repositories;

namespace ToDo.Domain.Interfaces.Databases;

public interface IUnitOfWork : IStateSaveChanges
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    
    IBaseRepository<User> Users { get; set; }
    
    IBaseRepository<Role> Roles { get; set; }
    
    IBaseRepository<UserRole> UserRoles { get; set; }
}