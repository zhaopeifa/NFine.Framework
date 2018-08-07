
namespace NFine.Domain.Entity.SystemManage
{
    using System;

    /// <summary>
    /// 评测城市
    /// </summary>
    public class ProfileCityEntity : IEntity<ProfileCityEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {

        public string F_Id { get; set; }

        public string CityName { get; set; }
        public string CityCode { get; set; }

        public string F_CreatorUserId { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        public bool? F_DeleteMark { get; set; }

        public string F_DeleteUserId { get; set; }

        public DateTime? F_DeleteTime { get; set; }


        public string F_LastModifyUserId { get; set; }

        public DateTime? F_LastModifyTime { get; set; }
    }
}
