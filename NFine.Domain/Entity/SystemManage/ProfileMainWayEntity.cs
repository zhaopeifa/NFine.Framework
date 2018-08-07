using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 环评-环评 主路
    /// </summary>
    public class ProfileMainWayEntity : IEntity<ProfileMainWayEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string F_EnCode { get; set; }

        /// <summary>
        /// 主路名称
        /// </summary>
        public string MainWayName { get; set; }

        /// <summary>
        /// 城市Id
        /// </summary>

        public string CityId { get; set; }

        /// <summary>
        /// 区县Id
        /// </summary>

        public string CountyId { get; set; }

        /// <summary>
        /// 街道Id
        /// </summary>

        public string StreetId { get; set; }

        public string F_CreatorUserId { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        public bool? F_DeleteMark { get; set; }

        public string F_DeleteUserId { get; set; }

        public DateTime? F_DeleteTime { get; set; }


        public string F_LastModifyUserId { get; set; }

        public DateTime? F_LastModifyTime { get; set; }
    }
}
