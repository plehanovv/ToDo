using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ToDo.Application.Resources;
using ToDo.Domain.Dto;
using ToDo.Domain.Dto.Report;
using ToDo.Domain.Entity;
using ToDo.Domain.Enum;
using ToDo.Domain.Interfaces.Repositories;
using ToDo.Domain.Interfaces.Services;
using ToDo.Domain.Interfaces.Validations;
using ToDo.Domain.Result;

namespace ToDo.Application.Services;

public class ReportService : IReportService
{
    private readonly IBaseRepository<Report> _reportRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IReportValidator _reportValidator;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    
    public ReportService(IBaseRepository<Report> reportRepository, IBaseRepository<User> userRepository, ILogger logger, IReportValidator reportValidator)
    {
        _reportRepository = reportRepository;
        _userRepository = userRepository;
        _logger = logger;
        _reportValidator = reportValidator;
    }

    /// <inheritdoc />
    public async Task<CollectionResult<ReportDto>> GetReportsAsync(long userId)
    {
        ReportDto[] reports;

        try
        {
            reports = await _reportRepository.GetAll()
                .Where(x => x.UserId == userId)
                .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                .ToArrayAsync();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new CollectionResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
        
        if (!reports.Any())
        {
            _logger.Warning(WarningMessage.ReportsNotFound, reports.Length);
            return new CollectionResult<ReportDto>()
            {
                ErrorMessage = WarningMessage.ReportsNotFound,
                ErrorCode = (int)ErrorCodes.ReportsNotFound
            };
        }
        
        return new CollectionResult<ReportDto>()
        {
            Data = reports,
            Count = reports.Length
        };
    }

    /// <inheritdoc />
    public async Task<BaseResult<ReportDto>> GetReportByIdAsync(long id)
    {
        ReportDto? report;

        try
        {
            report = await _reportRepository.GetAll()
                .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
        
        if (report == null)
        {
            _logger.Warning($"Отчет с {id} не найден", id);
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = WarningMessage.ReportNotFound,
                ErrorCode = (int)ErrorCodes.ReportsNotFound
            };
        }
        
        return new BaseResult<ReportDto>()
        {
            Data = report
        };
    }

    /// <inheritdoc />
    public async Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.UserId);
            var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
            var result = _reportValidator.CreateValidator(report, user);
            if (!result.IsSuccess)
            {
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode
                };
            }

            report = new Report()
            {
                Name = dto.Name,
                Description = dto.Description,
                UserId = user.Id,
            };
            
            await _reportRepository.CreateAsync(report);
            return new BaseResult<ReportDto>()
            {
                Data = _mapper.Map<ReportDto>(report)
            };
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }

    /// <inheritdoc />
    public async Task<BaseResult<ReportDto>> DeleteReportAsync(long id)
    {
        try
        {
            var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            var result = _reportValidator.ValidateOnNull(report);
            if (!result.IsSuccess)
            {
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode
                };
            }
            await _reportRepository.RemoveAsync(report);
            return new BaseResult<ReportDto>()
            {
                Data = _mapper.Map<ReportDto>(report)
            };
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }

    /// <inheritdoc />
    public async Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto)
    {
        try
        {
            var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
            var result = _reportValidator.ValidateOnNull(report);
            if (!result.IsSuccess)
            {
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode
                };
            }
            
            report.Name = dto.Name;
            report.Description = dto.Description;
            
            await _reportRepository.UpdateAsync(report);
            return new BaseResult<ReportDto>()
            {
                Data = _mapper.Map<ReportDto>(report)
            };
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }
}