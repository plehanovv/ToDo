using ToDo.Domain.Result;

namespace ToDo.Domain.Interfaces.Validations;

public interface IBaseValidator<in T> where T : class
{
    BaseResult ValidateOnNull(T model);
}