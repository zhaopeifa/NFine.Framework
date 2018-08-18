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

        /// <summary>
        /// 街道Id
        /// </summary>
        public string StreetId { get; set; }

        

        /// <summary>
        /// 街道名称
        /// </summary>
        public string StreetName { get; set; }

        public string WayName { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public bool CompleteState { get; set; }

        public string PersonInChargeId { get; set; }

        public string PersonInChargeName { get; set; }

        public string OrdeNo { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime CompletionTime { get; set; }

        /// <summary>
        /// 派发时间
        /// </summary>
        public DateTime? DeliveryTime { get; set; }

    }
}
