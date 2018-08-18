using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Contracts
{
    public interface ITaskDetail
    {
        /// <summary>
        /// 当前任务完成状态
        /// </summary>
        bool CompleteState { get; set; }

        /// <summary>
        /// 被派发人Id
        /// </summary>
        string PersonInChargeId { get; set; }

        /// <summary>
        /// 被派发人名称
        /// </summary>
        string PersonInChargeName { get; set; }

        /// <summary>
        /// 街道Id
        /// </summary>
        string StreetId { get; set; }

        /// <summary>
        /// 街道名称
        /// </summary>
        string StreetName { get; set; }
       
        /// <summary>
        /// 抽样单号
        /// </summary>
        string OrdeNo { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        DateTime CompletionTime { get; set; }

        /// <summary>
        /// 派发时间
        /// </summary>
        DateTime? DeliveryTime { get; set; }

    }
}
