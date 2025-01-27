namespace ToDo.Domain.Enum;

public enum ErrorCodes
{
    ReportsNotFound = 0,
    ReportNotFound = 1,
    ReportAlreadyExists = 2,
    
    InternalServerError = 10,
    
    UserNotFound = 11,
}