using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Contracts
{
    /// <summary>
    /// 任务当中筛选检查点数量
    /// </summary>
    public class TaskScreeningCheckPostCountContracts
    {
        /// <summary>
        /// 公厕数量
        /// </summary>
        public int TandasCount { get; set; }

        /// <summary>
        /// 垃圾箱房数量
        /// </summary>
        public int GarbageBox { get; set; }

        /// <summary>
        /// 压缩站数量
        /// </summary>
        public int compressionStation { get; set; }

        /// <summary>
        /// 倒粪池小便池
        /// </summary>
        public int CesspoolCount { get; set; }

        /// <summary>
        /// 沿途绿化数量
        /// </summary>
        public int Greening { get; set; }

        /// <summary>
        /// 绿色账户小区数量
        /// </summary>
        public int GreenResidential { get; set; }
    }
}
