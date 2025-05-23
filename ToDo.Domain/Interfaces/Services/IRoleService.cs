using ToDo.Domain.Dto.Role;
using ToDo.Domain.Dto.UserRole;
using ToDo.Domain.Entity;
using ToDo.Domain.Result;

namespace ToDo.Domain.Interfaces.Services;

/// <summary>
/// Сервис предназначенный для управлением ролей
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// Создание роли
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<RoleDto>> CreateRoleAsync(CreateRoleDto dto);
    
    /// <summary>
    /// Удаление роли
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<RoleDto>> DeleteRoleAsync(long id);
    
    /// <summary>
    /// Обновление роли
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<RoleDto>> UpdateRoleAsync(RoleDto dto);
    
    /// <summary>
    /// Добавление роли для пользователя
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<UserRoleDto>> AddRoleForUserAsync(UserRoleDto dto);
    
    /// <summary>
    /// Удаление роли у пользователя
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<UserRoleDto>> DeleteRoleForUserAsync(DeleteUserRoleDto dto);
    
    /// <summary>
    /// Обновление роли у пользователя
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<UserRoleDto>> UpdateRoleForUserAsync(UpdateUserRoleDto dto);
}