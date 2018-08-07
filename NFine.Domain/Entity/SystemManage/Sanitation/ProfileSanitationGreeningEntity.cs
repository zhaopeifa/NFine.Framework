using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 环评-环卫-沿途绿化
    /// </summary>
    public class ProfileSanitationGreeningEntity : IEntity<ProfileSanitationGreeningEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string F_EnCode { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 起点
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// 终点
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// 等级 类型
        /// </summary>
        public int GreeningGrade { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string CityId { get; set; }

        /// <summary>
        /// 区县
        /// </summary>
        public string CountyId { get; set; }

        /// <summary>
        /// 街道
        /// </summary>
        public string StreetId { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public string ProjectId { get; set; }

        public string F_CreatorUserId { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        public bool? F_DeleteMark { get; set; }

        public string F_DeleteUserId { get; set; }

        public DateTime? F_DeleteTime { get; set; }


        public string F_LastModifyUserId { get; set; }

        public DateTime? F_LastModifyTime { get; set; }
    }
}
