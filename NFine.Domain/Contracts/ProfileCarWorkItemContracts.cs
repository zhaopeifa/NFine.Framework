using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Contracts
{
    /// <summary>
    /// 环卫作业车辆
    /// </summary>
    public class ProfileCarWorkItemContracts
    {
        public string id { get; set; }
        public int subscript { get; set; }

        public string time { get; set; }

        public string rinseName { get; set; }

        public string rinseAddress { get; set; }

        public int rinseLength { get; set; }
    }
}
