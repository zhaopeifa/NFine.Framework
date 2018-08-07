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
    public class ProfileSanitationCarEntity : IEntity<ProfileSanitationCarEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        /// <summary>
        /// 下标编号
        /// </summary>
        public int Subscript { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string F_EnCode { get; set; }

        /// <summary>
        /// 关联机扫车Id
        /// </summary>
        public string MachineCleanCarId { get; set; }

        /// <summary>
        /// 时间段
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 主路Id
        /// </summary>
        public string MainId { get; set; }

        /// <summary>
        /// 起点
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// 终点
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        public string BYMESS1 { get; set; }

        public string BYMESS2 { get; set; }

        public string BYMESS3 { get; set; }

        public string BYMESS4 { get; set; }

        public string F_CreatorUserId { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        public bool? F_DeleteMark { get; set; }

        public string F_DeleteUserId { get; set; }

        public DateTime? F_DeleteTime { get; set; }

        public string F_LastModifyUserId { get; set; }

        public DateTime? F_LastModifyTime { get; set; }
    }
}
