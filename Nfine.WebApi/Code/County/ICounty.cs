using Nfine.WebApi.Contracts;
using NFine.Domain.Contracts;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfine.WebApi.Code.County
{
    public interface ICounty
    {
        List<ApiProfileCountyContracts> GetProfileCountyEntitys(string cityId);
    }
}
