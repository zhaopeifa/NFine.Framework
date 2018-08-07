using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 环评-市容-水域
    /// </summary>
    public class ProfileAmenitiesWatersEntity : IEntity<ProfileAmenitiesWatersEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string F_EnCode { get; set; }

        /// <summary>
        /// 样本点序列号
        /// </summary>
        public string SampleCode { get; set; }

        /// <summary>
        /// 水域名称
        /// </summary>
        public string WatersName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 水域类型
        /// </summary>
        public int WatersType { get; set; }

        /// <summary>
        /// 是否为界河
        /// </summary>
        public bool IsBoundaryRiver { get; set; }

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
