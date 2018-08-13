using Nfine.WebApi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfine.WebApi.Code.Task
{
    public interface ITask
    {
        Contracts.ApiWayContracts[] GetWayTask(string UserId, CheckPointClassificationEnum type);
    }
}
