using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 环评-评分标准-分类关系表  用于建了基础数据与评分标准关联
    /// </summary>
    public class ProfileGrading_Type_RlationEntity : IEntity<ProfileGrading_Type_RlationEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        /// <summary>
        /// 基础数据类型  如 道路，垃圾箱房
        /// </summary>
        public int ProfileGradeBasicType { get; set; }

        /// <summary>
        /// 基础数据当中的 类型 如  道路的一级道路，二级道路
        /// </summary>
        public string ProfileGradeType { get; set; }

        /// <summary>
        /// 关联评分标准小类
        /// </summary>
        public string ProfileGradingTypeId { get; set; }

        public string F_CreatorUserId { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        public bool? F_DeleteMark { get; set; }

        public string F_DeleteUserId { get; set; }

        public DateTime? F_DeleteTime { get; set; }


        public string F_LastModifyUserId { get; set; }

        public DateTime? F_LastModifyTime { get; set; }
    }
}
