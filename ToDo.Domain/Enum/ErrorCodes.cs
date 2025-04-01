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
    UserUnauthorizedAccess = 13,
    UserAlreadyExistsThisRole = 14,
    
    // 21-30 Авторизация и регистрация
    PasswordNotEqualsPasswordConfirm = 21,
    PasswordIsWrong = 22,
    
    // 31-35 Роли
    RoleAlreadyExists = 31,
    RoleNotFound = 32,
}