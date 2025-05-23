using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Domain.Dto.Role;
using ToDo.Domain.Dto.UserRole;
using ToDo.Domain.Entity;
using ToDo.Domain.Interfaces.Services;
using ToDo.Domain.Result;

namespace ToDo.Api.Controllers;

[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// Создание роли
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST
    ///     {
    ///         "name": "User",
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Если роль создалась</response>
    /// <response code="400">Если роль не создалась</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> Create([FromBody] CreateRoleDto dto)
    {
        var response = await _roleService.CreateRoleAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    /// <summary>
    /// Удаление роли
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE
    ///     {
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Если роль удалилась</response>
    /// <response code="400">Если роль не удалилась</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> Delete([FromBody] long id)
    {
        var response = await _roleService.DeleteRoleAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    /// <summary>
    /// Обновление роли
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT
    ///     {
    ///         "id": "1",
    ///         "name": "Admin",
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Если роль обновилась</response>
    /// <response code="400">Если роль не обновилась</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> Update([FromBody] RoleDto dto)
    {
        var response = await _roleService.UpdateRoleAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    /// <summary>
    /// Добавление роли пользователю
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST
    ///     {
    ///         "login": "user",
    ///         "roleName": "Admin",
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Если роль добавлена</response>
    /// <response code="400">Если роль не добавлена</response>
    [HttpPost("add-role")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> AddRoleForUser([FromBody] UserRoleDto dto)
    {
        var response = await _roleService.AddRoleForUserAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    /// <summary>
    /// Удаление роли у пользователя
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE
    ///     {
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Если роль удалилась</response>
    /// <response code="400">Если роль не удалилась</response>
    [HttpDelete("delete-role")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> DeleteRoleForUserAsync([FromBody] DeleteUserRoleDto dto)
    {
        var response = await _roleService.DeleteRoleForUserAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    /// <summary>
    /// Обновление роли у пользователя
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT
    ///     {
    ///         "id": "1",
    ///         "name": "Admin",
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Если роль обновилась</response>
    /// <response code="400">Если роль не обновилась</response>
    [HttpPut("update-role")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> UpdateRoleForUserAsync([FromBody] UpdateUserRoleDto dto)
    {
        var response = await _roleService.UpdateRoleForUserAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}