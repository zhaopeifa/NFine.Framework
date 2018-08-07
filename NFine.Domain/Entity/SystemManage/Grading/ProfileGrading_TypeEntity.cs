using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{

    /// <summary>
    /// 环评-评分标准-小类
    /// </summary>
    public class ProfileGrading_TypeEntity : IEntity<ProfileMainWayEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {

        public string F_Id { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分值
        /// </summary>
        public int Grade { get; set; }
        public string F_CreatorUserId { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        public bool? F_DeleteMark { get; set; }

        public string F_DeleteUserId { get; set; }

        public DateTime? F_DeleteTime { get; set; }


        public string F_LastModifyUserId { get; set; }

        public DateTime? F_LastModifyTime { get; set; }
    }
}
