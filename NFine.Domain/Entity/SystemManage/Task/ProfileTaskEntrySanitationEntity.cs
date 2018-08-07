using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 环评-环卫-任务条目
    /// </summary>
    public class ProfileTaskEntrySanitationEntity : IEntity<ProfileTaskEntrySanitationEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        /// <summary>
        /// 任务Id
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// 编号
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
        /// 公司Id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 街道Id
        /// </summary>
        public string StreetId { get; set; }

        /// <summary>
        /// 被派发人 责任人
        /// </summary>
        public string PersonInChargeId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 任务条目 关联类型
        /// </summary>
        public string TaskEntryType { get; set; }

        /// <summary>
        /// 关联数据Id
        /// </summary>
        public string EntryDataId { get; set; }

        public string F_CreatorUserId { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        public bool? F_DeleteMark { get; set; }

        public string F_DeleteUserId { get; set; }

        public DateTime? F_DeleteTime { get; set; }


        public string F_LastModifyUserId { get; set; }

        public DateTime? F_LastModifyTime { get; set; }
    }
}
