using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Authentication.Application.ViewModels.Request;
using Unidas.MS.Authentication.Application.ViewModels.SalesForce;

namespace Unidas.MS.Authentication.Application.Interfaces.Services
{
    public interface ISalesForceService
    {
        Task<RetornoToken> Authorize(CredentialsViewModel model);
    }
}
