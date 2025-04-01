using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Resources;
using ToDo.Domain.Dto.Role;
using ToDo.Domain.Dto.UserRole;
using ToDo.Domain.Entity;
using ToDo.Domain.Enum;
using ToDo.Domain.Interfaces.Databases;
using ToDo.Domain.Interfaces.Repositories;
using ToDo.Domain.Interfaces.Services;
using ToDo.Domain.Result;

namespace ToDo.Application.Services;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<Role> _roleRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<UserRole> _userRoleRepository;
    private readonly IMapper _mapper;

    public RoleService(IBaseRepository<Role> roleRepository, IBaseRepository<User> userRepository, 
        IMapper mapper, IBaseRepository<UserRole> userRoleRepository, IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _userRoleRepository = userRoleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResult<RoleDto>> CreateRoleAsync(CreateRoleDto dto)
    {
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
        if (role != null)
        {
            return new BaseResult<RoleDto>()
            {
                ErrorMessage = ErrorMessage.RoleAlreadyExists,
                ErrorCode = (int)ErrorCodes.RoleAlreadyExists
            };
        }

        role = new Role()
        {
            Name = dto.Name,
        };
        
        await _roleRepository.CreateAsync(role);
        
        return new BaseResult<RoleDto>()
        {
            Data = _mapper.Map<RoleDto>(role)
        };
    }

    public async Task<BaseResult<RoleDto>> DeleteRoleAsync(long id)
    {
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        if (role == null)
        {
            return new BaseResult<RoleDto>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode = (int)ErrorCodes.RoleNotFound
            };
        }
        
        _roleRepository.Remove(role);
        await _roleRepository.SaveChangesAsync();
        
        return new BaseResult<RoleDto>()
        {
            Data = _mapper.Map<RoleDto>(role)
        };
    }

    public async Task<BaseResult<RoleDto>> UpdateRoleAsync(RoleDto dto)
    {
        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (role == null)
        {
            return new BaseResult<RoleDto>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode = (int)ErrorCodes.RoleNotFound
            };
        }
        
        role.Name = dto.Name;
        var updatedRole = _roleRepository.Update(role);
        await _roleRepository.SaveChangesAsync();
        
        return new BaseResult<RoleDto>()
        {
            Data = _mapper.Map<RoleDto>(updatedRole)
        };
    }

    public async Task<BaseResult<UserRoleDto>> AddRoleForUserAsync(UserRoleDto dto)
    {
        var user = await _userRepository.GetAll()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Login == dto.Login);
        
        if (user == null)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode = (int)ErrorCodes.UserNotFound
            };
        }

        var roles = user.Roles.Select(x => x.Name).ToArray();

        if (roles.All(x => x != dto.RoleName))
        {
            var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.RoleName);
            if (role == null)
            {
                return new BaseResult<UserRoleDto>()
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErrorCode = (int)ErrorCodes.RoleNotFound
                };
            }

            UserRole userRole = new UserRole()
            {
                RoleId = role.Id,
                UserId = user.Id
            };
            
            await _userRoleRepository.CreateAsync(userRole);

            return new BaseResult<UserRoleDto>()
            {
                Data = new UserRoleDto()
                {
                    Login = user.Login,
                    RoleName = role.Name
                }
            };
        }

        return new BaseResult<UserRoleDto>()
        {
            ErrorMessage = ErrorMessage.UserAlreadyExistsThisRole,
            ErrorCode = (int)ErrorCodes.UserAlreadyExistsThisRole
        };
    }

    public async Task<BaseResult<UserRoleDto>> DeleteRoleForUserAsync(DeleteUserRoleDto dto)
    {
        var user = await _userRepository.GetAll()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Login == dto.Login);
        
        if (user == null)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode = (int)ErrorCodes.UserNotFound
            };
        }
        
        var role = user.Roles.FirstOrDefault(x => x.Id == dto.RoleId);
        if (role == null)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode = (int)ErrorCodes.RoleNotFound
            };
        }
        
        var userRole = _userRoleRepository.GetAll()
            .Where(x => x.RoleId == role.Id)
            .FirstOrDefault(x => x.UserId == user.Id);
        
        _userRoleRepository.Remove(userRole);
        await _userRoleRepository.SaveChangesAsync();
        
        return new BaseResult<UserRoleDto>()
        {
            Data = new UserRoleDto()
            {
                Login = user.Login,
                RoleName = role.Name
            }
        };
    }

    public async Task<BaseResult<UserRoleDto>> UpdateRoleForUserAsync(UpdateUserRoleDto dto)
    {
        var user = await _userRepository.GetAll()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Login == dto.Login);
        
        if (user == null)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode = (int)ErrorCodes.UserNotFound
            };
        }
        
        var role = user.Roles.FirstOrDefault(x => x.Id == dto.FromRoleId);
        if (role == null)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode = (int)ErrorCodes.RoleNotFound
            };
        }
        
        var newRoleFromUser = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.FromRoleId);
        if (newRoleFromUser == null)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode = (int)ErrorCodes.RoleNotFound
            };
        }

        using (var transaction = await _unitOfWork.BeginTransactionAsync())
        {
            try
            {
                var userRole = await _unitOfWork.UserRoles.GetAll()
                    .Where(x => x.RoleId == role.Id)
                    .FirstAsync(x => x.UserId == user.Id);
                
                _unitOfWork.UserRoles.Remove(userRole);
                await _unitOfWork.SaveChangesAsync();

                var newUserRole = new UserRole()
                {
                    UserId = user.Id,
                    RoleId = newRoleFromUser.Id
                };
                
                await _unitOfWork.UserRoles.CreateAsync(newUserRole);
                await _unitOfWork.SaveChangesAsync();
                
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }
        
        return new BaseResult<UserRoleDto>()
        {
            Data = new UserRoleDto()
            {
                Login = user.Login,
                RoleName = newRoleFromUser.Name
            }
        };
    }
}