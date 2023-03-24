using FluentValidation;
using HttpServer.viewModels;

namespace HttpServer.validators;

public class CreateEmployeeValidator : AbstractValidator<EmployeeViewModel>
{
    public CreateEmployeeValidator()
    {
        RuleFor(employee => employee.Name)
            .MaximumLength(15)
            .WithMessage("Допустимо не более 10 символов")
            .MinimumLength(1)
            .WithMessage("Имя должно содержать не менее одной буквы")
            .Must(c => c.All(Char.IsLetter)).WithMessage("Имя не должно содержать сторонние символы");
        RuleFor(employee => employee.Surname)
            .MaximumLength(25)
            .WithMessage("Допустимо не более 25 символов")
            .MinimumLength(1)
            .WithMessage("Фамилия должна содержать не менее одной буквы")
            .Must(c => c.All(Char.IsLetter)).WithMessage("Фамилия не должна содержать сторонние символы");
        RuleFor(employee => employee.Age)
            .GreaterThan(14).WithMessage("Возраст должен превышать 14 лет")
            .LessThan(180).WithMessage("Возраст не должен превышать 180 лет");
        RuleFor(employee => employee.About)
            .MaximumLength(100)
            .WithMessage("Не более 100 символов")
            .MinimumLength(10)
            .WithMessage("Не менее 10 символов");
    }
}