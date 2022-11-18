using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidas.MS.Authentication.Application.ViewModels
{
    public class AppSettings
    {
        public SalesForceInfo SalesForce { get; set; } = new SalesForceInfo();
    }

    public class SalesForceInfo
    {
        public string Url { get; set; }
    }
}
