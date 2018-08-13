using Nfine.WebApi.Contracts;
using Nfine.WebApi.Data.Enums;
using NFine.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfine.WebApi.Code.CheckingPoint
{
    public interface ICheckingPoint
    {
        List<ApiCheckingPointContracts> GetCheckingPoint(string ProjectId);
        List<ApiCheckingPointClassification> GetCheckingPointClassification(CheckPointTypeEnum CheckPointCode);
    }
}
