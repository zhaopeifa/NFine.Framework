using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Enums
{
    /// <summary>
    /// 环评-任务状态
    /// </summary>
    public enum ProfileTaskStateEnum
    {
        为派遣 = 0,
        已派遣 = 1,
        待审核 = 2,
        已终结 = 3,
        已作废 = -1
    }
}
