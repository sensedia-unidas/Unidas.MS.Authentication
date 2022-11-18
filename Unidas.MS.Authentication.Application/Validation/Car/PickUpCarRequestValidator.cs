using FluentValidation;
using MsSensediaTemplate.Application.ViewModels.Car;
using MsSensediaTemplate.Application.ViewModels.Car.Requests;

namespace MsSensediaTemplate.Application.Validation.Car
{
    public class PickUpCarRequestValidator : AbstractValidator<PickupCarRequest>
    {
        public PickUpCarRequestValidator()
        {
            RuleFor(m => m.CarId).NotEmpty();
            RuleFor(m => m.RentedBy).NotEmpty();
        }
    }
}
