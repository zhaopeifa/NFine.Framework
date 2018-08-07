using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 环卫作业公司
    /// </summary>
    public class ProfileOperationsCompanyEntity : IEntity<ProfileOperationsCompanyEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {

        public string F_Id { get; set; }

        public string CompanyName { get; set; }

        public int CompanyType { get; set; }

        public string HasVehicleTypeIds { get; set; }

        public string F_CreatorUserId { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        public bool? F_DeleteMark { get; set; }

        public string F_DeleteUserId { get; set; }

        public DateTime? F_DeleteTime { get; set; }


        public string F_LastModifyUserId { get; set; }

        public DateTime? F_LastModifyTime { get; set; }
    }
}
