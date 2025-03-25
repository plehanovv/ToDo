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

    
    public ReportService(IBaseRepository<Report> reportRepository, IBaseRepository<User> userRepository, 
        ILogger logger, IReportValidator reportValidator, IMapper mapper)
    {
        _reportRepository = reportRepository;
        _userRepository = userRepository;
        _logger = logger;
        _reportValidator = reportValidator;
        _mapper = mapper;
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
    public Task<BaseResult<ReportDto>> GetReportByIdAsync(long id)
    {
        ReportDto? report;

        try
        {
            report = _reportRepository.GetAll()
                .AsEnumerable()
                .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                .FirstOrDefault(x => x.Id == id);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return Task.FromResult(new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            });
        }
        
        if (report == null)
        {
            _logger.Warning($"Отчет с {id} не найден", id);
            return Task.FromResult(new BaseResult<ReportDto>()
            {
                ErrorMessage = WarningMessage.ReportNotFound,
                ErrorCode = (int)ErrorCodes.ReportsNotFound
            });
        }
        
        return Task.FromResult(new BaseResult<ReportDto>()
        {
            Data = report
        });
    }

    /// <inheritdoc />
    public async Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto)
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

    /// <inheritdoc />
    public async Task<BaseResult<ReportDto>> DeleteReportAsync(long id)
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
        _reportRepository.Remove(report);
        await _reportRepository.SaveChangesAsync();
        return new BaseResult<ReportDto>()
        {
            Data = _mapper.Map<ReportDto>(report)
        };
    }

    /// <inheritdoc />
    public async Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto)
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
            
        var updatedReport = _reportRepository.Update(report);
        await _reportRepository.SaveChangesAsync();
        return new BaseResult<ReportDto>()
        {
            Data = _mapper.Map<ReportDto>(updatedReport)
        };
    }
}