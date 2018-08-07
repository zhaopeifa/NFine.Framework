using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 区县
    /// </summary>
    public class ProfileCountyEntity : IEntity<ProfileCountyEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        public string CountyName { get; set; }

        public string CountyCode { get; set; }

        /// <summary>
        /// 城市Id 当前区县所在的城市
        /// </summary>
        public string CityId { get; set; }

        public string F_CreatorUserId { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        public bool? F_DeleteMark { get; set; }

        public string F_DeleteUserId { get; set; }

        public DateTime? F_DeleteTime { get; set; }


        public string F_LastModifyUserId { get; set; }

        public DateTime? F_LastModifyTime { get; set; }
    }
}
