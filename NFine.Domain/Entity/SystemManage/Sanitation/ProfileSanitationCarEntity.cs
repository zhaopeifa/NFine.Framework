using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 环评-环卫-机扫车
    /// </summary>
    public class ProfileSanitationCarEntity : IEntity<ProfileSanitationCarEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {

        public string F_Id { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public int CarType { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string F_EnCode { get; set; }

        /// <summary>
        /// 班组名称
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// 所属公司
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 班组人数
        /// </summary>
        public int? TeamPeopleCount { get; set; }

        /// <summary>
        /// 机扫路段
        /// </summary>
        public string CleanRoad { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarId { get; set; }

        /// <summary>
        /// 车队地址
        /// </summary>
        public string CarTeamAddress { get; set; }

        /// <summary>
        /// 司机
        /// </summary>
        public string Driver { get; set; }

        public string F_CreatorUserId { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        public bool? F_DeleteMark { get; set; }

        public string F_DeleteUserId { get; set; }

        public DateTime? F_DeleteTime { get; set; }


        public string F_LastModifyUserId { get; set; }

        public DateTime? F_LastModifyTime { get; set; }
    }
}
