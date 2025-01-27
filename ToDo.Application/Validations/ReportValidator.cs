using ToDo.Application.Resources;
using ToDo.Domain.Entity;
using ToDo.Domain.Enum;
using ToDo.Domain.Interfaces.Validations;
using ToDo.Domain.Result;

namespace ToDo.Application.Validations;

public class ReportValidator : IReportValidator
{
    public BaseResult ValidateOnNull(Report model)
    {
        if (model == null)
        {
            return new BaseResult()
            {
                ErrorMessage = WarningMessage.ReportNotFound,
                ErrorCode = (int)ErrorCodes.ReportNotFound
            };
        }

        return new BaseResult();
    }

    public BaseResult CreateValidator(Report report, User user)
    {
        if (report != null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.ReportAlreadyExists,
                ErrorCode = (int)ErrorCodes.ReportAlreadyExists
            };
        }

        if (user == null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode = (int)ErrorCodes.UserNotFound
            };
        }
        
        return new BaseResult();
    }
}