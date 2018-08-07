using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 环评-环卫-绿色账户小区
    /// </summary>
    public class ProfileSanitationGreenResidentialEntity : IEntity<ProfileSanitationGreenResidentialEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string F_EnCode { get; set; }

        /// <summary>
        /// 小区名
        /// </summary>
        public string ResidentialName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 绿色账户点位数
        /// </summary>
        public int SomeDigits { get; set; }

        /// <summary>
        /// 兑换时间
        /// </summary>
        public string ExchangeTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 物业名称
        /// </summary>
        public string PropertyName { get; set; }

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
        /// 项目id
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
