using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Domain.Enums;

namespace NFine.Domain.Contracts
{
    /// <summary>
    /// 任务单列表显示model
    /// </summary>
    public class ProfileTaskContracts
    {
        private string _StateName = string.Empty;

        /// <summary>
        /// 主键Id
        /// </summary>
        public string F_Id { get; set; }

        /// <summary>
        /// 当前任务单状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 当前任务单状态名
        /// </summary>
        public string StateName
        {
            get
            {
                if (string.IsNullOrEmpty(this._StateName))
                {
                    _StateName = ((NFine.Domain.Enums.ProfileTaskStateEnum)this.State).GetAnnotation();
                }
                return _StateName;
            }
        }

        /// <summary>
        /// 抽样单号
        /// </summary>
        public string F_EnCode { get; set; }

        /// <summary>
        /// 城市Id
        /// </summary>
        public string CityId { get; set; }

        /// <summary>
        /// 区县Id
        /// </summary>
        public string CountyId { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public int ProjectType { get; set; }

        /// <summary>
        /// 环卫公司id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 街道Id
        /// </summary>
        public string StreetId { get; set; }

        /// <summary>
        /// 被派发人Id
        /// </summary>
        public string PersonInChargeId { get; set; }

        /// <summary>
        /// 被派发人名
        /// </summary>
        public string PersonInChargeRealName { get; set; }

        /// <summary>
        /// 派发时间
        /// </summary>
        public DateTime? DeliveryTime { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime CompletionTime { get; set; }

    }
}
