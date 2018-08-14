using NFine.Data;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.SystemManage
{
    /// <summary>
    /// 环评-市容-在建工地
    /// </summary>
    public class ProfileAmenitiesConstructionSiteRepository : RepositoryBase<ProfileAmenitiesConstructionSiteEntity>
    {

        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                var delEntity = db.FindEntity<ProfileAmenitiesConstructionSiteEntity>(keyValue);
                db.Delete<ProfileAmenitiesConstructionSiteEntity>(delEntity);

                db.Commit();
            }
        }

        public void SubmitForm(ProfileAmenitiesConstructionSiteEntity Entity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {

                if (!string.IsNullOrEmpty(keyValue))//修改
                {
                    //删除之前无用数据
                    db.Update(Entity);
                   
                }
                else
                {
                    db.Insert(Entity);

                   
                }
                db.Commit();
            }
        }
    }
}
