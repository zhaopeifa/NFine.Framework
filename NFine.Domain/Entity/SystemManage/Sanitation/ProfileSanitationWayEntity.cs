using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    public class ProfileSanitationWayEntity : IEntity<ProfileSanitationWayEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }

        /// <summary>
        /// 道路名
        /// </summary>
        public string WayName { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string F_EnCode { get; set; }

        /// <summary>
        /// 起点
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// 终点
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// 路级
        /// </summary>
        public int WayGrade { get; set; }

        /// <summary>
        /// 是否结合部道路
        /// </summary>
        public bool IsJointPartWay { get; set; }

        /// <summary>
        /// 主路Id
        /// </summary>
        public string MainWayId { get; set; }

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
