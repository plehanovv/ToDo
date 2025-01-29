using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Domain.Dto.Report;
using ToDo.Domain.Interfaces.Services;
using ToDo.Domain.Result;

namespace ToDo.Api.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("reports/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> GetUserReports(long userId)
    {
        var response = await _reportService.GetReportsAsync(userId);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> GetReport(long id)
    {
        var response = await _reportService.GetReportByIdAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> Delete(long id)
    {
        var response = await _reportService.DeleteReportAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    /// <summary>
    /// Создание отчета
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Request for create report
    /// 
    ///     POST
    ///     {
    ///         "name": "Report #1",
    ///         "description": "Test report",
    ///         "userId": 1
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Если отчет создался</response>
    /// <response code="400">Если отчет не создался</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> Create([FromBody] CreateReportDto dto)
    {
        var response = await _reportService.CreateReportAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> Update([FromBody] UpdateReportDto dto)
    {
        var response = await _reportService.UpdateReportAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}