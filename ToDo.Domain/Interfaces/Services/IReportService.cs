using ToDo.Domain.Dto;
using ToDo.Domain.Result;

namespace ToDo.Domain.Interfaces.Services;

/// <summary>
/// Сервис отвечающий зза работу с доменной частью отчета Report
/// </summary>
public interface IReportService
{
    /// <summary>
    /// Получение всех отчетов пользователя
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<CollectionResult<ReportDto>> GetReportsAsync(long userId);
}