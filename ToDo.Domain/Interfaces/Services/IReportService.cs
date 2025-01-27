using ToDo.Domain.Dto.Report;
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
    
    /// <summary>
    /// Получение отчета по id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> GetReportByIdAsync(long id);
    
    /// <summary>
    /// Создание нового отчета с базовыми параметрами
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto);
    
    /// <summary>
    /// Удаление отчета по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> DeleteReportAsync(long id);
    
    /// <summary>
    /// Обновление отчета по id
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto);
}