using ToDo.Domain.Entity;
using ToDo.Domain.Result;

namespace ToDo.Domain.Interfaces.Validations;

public interface IReportValidator : IBaseValidator<Report>
{
    /// <summary>
    /// Проверяется наличие отчета, если отчет существует - создать такой же отчет нельзя
    /// Проверяется пользователь, если UserId не найден, то такого пользователя нет
    /// </summary>
    /// <param name="report"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    BaseResult CreateValidator(Report report, User user);
}