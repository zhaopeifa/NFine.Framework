using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 环评-市容-主路三年治理计划中间表
    /// </summary>
    public class ProfileAmenitiesMainWay_GovernEntity : IEntity<ProfileAmenitiesMainWay_GovernEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        /// <summary>
        /// 三年治理计划Id
        /// </summary>
        public string GovernId { get; set; }

        /// <summary>
        /// 主路Id
        /// </summary>
        public string MainWayId { get; set; }

        public string F_CreatorUserId { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        public bool? F_DeleteMark { get; set; }

        public string F_DeleteUserId { get; set; }

        public DateTime? F_DeleteTime { get; set; }


        public string F_LastModifyUserId { get; set; }

        public DateTime? F_LastModifyTime { get; set; }
    }
}
