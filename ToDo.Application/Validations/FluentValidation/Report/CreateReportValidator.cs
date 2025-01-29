using FluentValidation;
using ToDo.Domain.Dto.Report;

namespace ToDo.Application.Validations.FluentValidation.Report;

public class CreateReportValidator : AbstractValidator<CreateReportDto>
{
    public CreateReportValidator()
    {
        RuleFor(x => x.Name ).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description ).NotEmpty().MaximumLength(1000);
    }
}