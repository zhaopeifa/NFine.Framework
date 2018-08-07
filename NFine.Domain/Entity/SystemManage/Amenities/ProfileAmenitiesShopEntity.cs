using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 环评-市容-一点一档
    /// </summary>
    public class ProfileAmenitiesShopEntity : IEntity<ProfileAmenitiesShopEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {

        public string F_Id { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string F_EnCode { get; set; }

        /// <summary>
        /// 店招名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 主路Id
        /// </summary>
        public string MainWayId { get; set; }

        /// <summary>
        /// 主路名称
        /// </summary>
        public string MainWayName { get; set; }

        /// <summary>
        /// 路段起始点
        /// </summary>
        public string RoadOrigin { get; set; }

        /// <summary>
        /// 路段终点
        /// </summary>
        public string RoadDestination { get; set; }


        /// <summary>
        /// 城市
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
