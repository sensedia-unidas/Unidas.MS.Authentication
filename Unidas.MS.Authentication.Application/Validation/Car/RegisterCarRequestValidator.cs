using FluentValidation;
using MsSensediaTemplate.Application.ViewModels.Car;
using MsSensediaTemplate.Application.ViewModels.Car.Requests;

namespace MsSensediaTemplate.Application.Validation.Car
{
    public class RegisterCarRequestValidator : AbstractValidator<RegisterCarRequest>
    {
        public RegisterCarRequestValidator()
        {
            RuleFor(m => m.Plate).NotEmpty();
            RuleFor(m => m.Description).NotEmpty();
        }
    }
}
