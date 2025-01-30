using ToDo.Domain.Dto;
using ToDo.Domain.Dto.User;
using ToDo.Domain.Result;

namespace ToDo.Domain.Interfaces.Services;

/// <summary>
/// Сервис предназначаненый для авторизации/регистрации
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<UserDto>> Register(RegisterUserDto dto);
    
    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<TokenDto>> Login(LoginUserDto dto);
}