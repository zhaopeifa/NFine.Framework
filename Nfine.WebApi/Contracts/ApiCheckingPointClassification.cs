using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfine.WebApi.Contracts
{
    /// <summary>
    /// api检查点类型
    /// </summary>
    public class ApiCheckingPointClassification
    {
        public int CheckPointCode { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
    }
}
