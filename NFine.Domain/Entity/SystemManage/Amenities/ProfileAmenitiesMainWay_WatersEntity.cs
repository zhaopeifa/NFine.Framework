using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 环卫-市容-主路水域中间表
    /// </summary>
    public class ProfileAmenitiesMainWay_WatersEntity : IEntity<ProfileAmenitiesMainWay_ResidentialEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        /// <summary>
        /// 水域Id
        /// </summary>
        public string WatersId { get; set; }

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
