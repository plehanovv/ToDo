namespace ToDo.Domain.Enum;

public enum ErrorCodes
{
    // 0-10 Отчеты
    ReportsNotFound = 0,
    ReportNotFound = 1,
    ReportAlreadyExists = 2,
    
    InternalServerError = 10,
    
    // 11-20 Пользоваель
    UserNotFound = 11,
    UserAlreadyExists = 12,
    
    // 21-30 Авторизация и регистрация
    PasswordNotEqualsPasswordConfirm = 21,
}