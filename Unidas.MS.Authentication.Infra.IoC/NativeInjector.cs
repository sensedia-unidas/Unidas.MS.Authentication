using MsSensediaTemplate.Application.Interfaces;
using MsSensediaTemplate.Application.Interfaces.Commands.PickUpCar;
using MsSensediaTemplate.Application.Interfaces.Commands.RegisterCar;
using MsSensediaTemplate.Application.Commands.PickupCar;
using MsSensediaTemplate.Application.Commands.RegisterCar;
using MsSensediaTemplate.Application.Validation;
using MsSensediaTemplate.Domain.Interfaces.Repositories;
using MsSensediaTemplate.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Unidas.MS.Authentication.Application.Interfaces.Services;
using Unidas.MS.Authentication.Application.Services;

namespace MsSensediaTemplate.Infra.IoC
{
    public class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //REPOSITORY
            services.AddScoped<ICarReadOnlyRepository, CarRepository>();
            services.AddScoped<ICarWriteOnlyRepository, CarRepository>();

            //SERVICE
            services.AddScoped<IPickUpCarUseCase, PickUpCarUseCase>();
            services.AddScoped<IRegisterCarUseCase, RegisterUseCase>();
            services.AddScoped<ISalesForceService, SalesForceService>();

            services.AddSingleton<InMemoryDbContext>();
            services.AddScoped<IMinimalValidator, MinimalValidator>();
        }
    }
}