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
        /// 完成时间
        /// </summary>
        DateTime CompletionTime { get; set; }

    }
}
