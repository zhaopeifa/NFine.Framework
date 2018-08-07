using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 环评-市容-三年治理计划
    /// </summary>
    public class ProfileAmenitiesGovernEntity : IEntity<ProfileAmenitiesGovernEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {

        public string F_Id { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string F_EnCode { get; set; }

        /// <summary>
        /// 治理分类
        /// </summary>
        public int GovernSort { get; set; }

        /// <summary>
        /// 环境类型
        /// </summary>
        public int EnvironmentType { get; set; }

        /// <summary>
        /// 治理类型
        /// </summary>
        public int GovernType { get; set; }

        /// <summary>
        /// 治理名称
        /// </summary>
        public string GovernName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

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
