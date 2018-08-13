using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 环评-环卫-机扫车工作项
    /// </summary>
    public class ProfileSanitationCarWorkItemEntity : IEntity<ProfileSanitationCarWorkItemEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        public string CarId { get; set; }

        /// <summary>
        /// 下标编号
        /// </summary>
        public int Subscript { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string F_EnCode { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 冲洗点名称
        /// </summary>
        public string RinseName { get; set; }

        /// <summary>
        /// 冲洗点地址
        /// </summary>
        public string RinseAddress { get; set; }

        /// <summary>
        /// 长度（米）
        /// </summary>
        public int RinseLength { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BYMESS1 { get; set; }

        public string F_CreatorUserId { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        public bool? F_DeleteMark { get; set; }

        public string F_DeleteUserId { get; set; }

        public DateTime? F_DeleteTime { get; set; }

        public string F_LastModifyUserId { get; set; }

        public DateTime? F_LastModifyTime { get; set; }
    }
}
