using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Contracts
{
    /// <summary>
    /// 任务明细道路
    /// </summary>
    public class TaskDetailWayContracts:ITaskDetail
    {

        public string F_Id { get; set; }

        /// <summary>
        /// 当前数据Id
        /// </summary>
        public string DataId { get; set; }

        public string WayName { get; set; }

        public string F_EnCode { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public bool CompleteState { get; set; }

        public string PersonInChargeId { get; set; }

        public string PersonInChargeName { get; set; }

        public DateTime CompletionTime { get; set; }
    }
}
