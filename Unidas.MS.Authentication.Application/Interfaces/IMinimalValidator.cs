using MsSensediaTemplate.Application.ViewModels;

namespace MsSensediaTemplate.Application.Interfaces
{
    public interface IMinimalValidator
    {
        ValidationResult Validate<T>(T model);
    }
}
