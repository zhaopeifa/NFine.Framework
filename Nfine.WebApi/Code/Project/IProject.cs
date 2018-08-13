using Nfine.WebApi.Contracts;
using NFine.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfine.WebApi.Code.Project
{
    public interface IProject
    {
        List<ApiProjectContracts> GetProject(string countyId = "");
    }
}
