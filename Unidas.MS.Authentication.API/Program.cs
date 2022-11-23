global using Microsoft.AspNetCore.Mvc;
global using Newtonsoft.Json;
global using System;
global using System.Linq;
global using System.Net;
using FluentValidation;
using MsSensediaTemplate.API.Helpers;
using MsSensediaTemplate.Application.Interfaces.Commands.PickUpCar;
using MsSensediaTemplate.Application.Interfaces.Commands.RegisterCar;
using MsSensediaTemplate.Application.Interfaces.Commands.PickUpCar;
using MsSensediaTemplate.Application.Interfaces.Commands.RegisterCar;
using MsSensediaTemplate.Application.ViewModels.Car;
using MsSensediaTemplate.Application.ViewModels.Car.Requests;
using MsSensediaTemplate.Infra.IoC;
using Microsoft.OpenApi.Models;
using Unidas.MS.Authentication.Application.ViewModels.Request;
using Unidas.MS.Authentication.Application.Interfaces.Services;
using Unidas.MS.Authentication.Application.ViewModels;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Configuracoes adicionadas - builder.services
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1", new OpenApiInfo() { Title = "API V1", Version = "V1.0" });
    //options.SwaggerDoc("V2", new OpenApiInfo() { Title = "API V2", Version = "V2.0" });
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.CustomSchemaIds(x => x.FullName);
});

NativeInjector.RegisterServices(builder.Services);


builder.Services.AddMvc(options =>
{
    //options.Filters.Add(typeof(DomainExceptionFilter));
    options.Filters.Add(typeof(ValidateActionFilterAttribute));
});
#endregion

var appSettings = new AppSettings();
builder.Configuration.Bind("AppSettings", appSettings);
builder.Services.AddSingleton(appSettings);

var app = builder.Build();

#region Configuracoes adicionadas - app
//var versionSet = app.NewApiVersionSet()
//                    .HasApiVersion(1.0)
//                    .ReportApiVersions()
//                    .Build();

app.UseMiddleware(typeof(ApiExceptionMiddleware));
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/V1/swagger.json", "V1.0");
        //options.SwaggerEndpoint($"/swagger/V2/swagger.json", "V2.0");
    });
}

app.UseHttpsRedirection();


#region Endpoints
//TODO: Versao pronta para versionar, porém o Swagger precisa ser implementado corretamente para separar as versoes e mostrar como chamar cada uma delas
//app.MapGet("/GetMessage", () => "This is an example of a minimal API").WithApiVersionSet(versionSet).MapToApiVersion(1.0);
//app.MapGet("/GetMessage", () => "2222222222 This is an example of a minimal API 2").WithApiVersionSet(versionSet).MapToApiVersion(2.0);
//app.MapGet("/GetText", () => "This is yet another example of a minimal API").WithApiVersionSet(versionSet).WithApiVersionSet(versionSet).IsApiVersionNeutral();

// GET /GetMessage?api-version=1.0
// GET /GetMessage


app.MapPost("/SalesForce/Authenticate", async (CredentialsViewModel request, ISalesForceService service) =>
{
    app.Logger.LogInformation($"Token solicitado", request);

    var response = await service.Authorize(request);

    switch (response.Status)
    {
        case HttpStatusCode.OK:
            return Results.Ok(response);

        case HttpStatusCode.BadRequest:
            return Results.BadRequest(response);

        case HttpStatusCode.Unauthorized:
            return Results.Unauthorized();

        case HttpStatusCode.NotFound:
            return Results.NotFound();

        default:
            return Results.Ok();
    }

    
    //return response;

})
    .Produces<CredentialsViewModel>()
    .ProducesProblem(HttpStatusCode.Unauthorized.GetHashCode())
    .ProducesProblem(HttpStatusCode.NotFound.GetHashCode());


#endregion


app.Run();


