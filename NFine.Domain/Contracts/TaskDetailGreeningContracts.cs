using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Contracts
{
    public class TaskDetailGreeningContracts : ITaskDetail
    {
        public string F_Id { get; set; }

        public string DataId { get; set; }

        public string F_EnCode { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 起点
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// 终点
        /// </summary>
        public string Destination { get; set; }

        public bool CompleteState { get; set; }

        public string PersonInChargeId { get; set; }

        public string PersonInChargeName { get; set; }

        public DateTime CompletionTime { get; set; }
    }
}
