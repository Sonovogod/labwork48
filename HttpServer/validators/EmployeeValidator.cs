using FluentValidation;
using HttpServer.viewModels;

namespace HttpServer.validators;

public class EmployeeValidator : AbstractValidator<EmployeeViewModel>
{
    public EmployeeValidator()
    {
        RuleFor(employee => employee.Age).InclusiveBetween(18, 99)
            .WithMessage("Работник должен быть старше 18 лет и младше 99");
        RuleFor(employee => employee.Name)
            .NotEmpty().WithMessage("Имя не может быть пустым")
            .Matches(@"^([a-zA-Zа-яА-Я]+)([a-zA-Zа-яА-Я\s]*)$").WithMessage("Имя содержит недопустимые символы.");
    }
}