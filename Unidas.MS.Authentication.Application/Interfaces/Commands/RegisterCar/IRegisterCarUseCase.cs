using MsSensediaTemplate.Application.ViewModels.Car.Results;

namespace MsSensediaTemplate.Application.Interfaces.Commands.RegisterCar
{
    public interface IRegisterCarUseCase
    {
        Task<RegisterCarResult> Execute(string description, string plate);
    }
}
