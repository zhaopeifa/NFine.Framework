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
    /// 环评-市容-居民小区
    /// </summary>
    public class ProfileAmenitiesResidentialRepository : RepositoryBase<ProfileAmenitiesResidentialEntity>
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                var delEntity = db.FindEntity<ProfileAmenitiesResidentialEntity>(keyValue);
                db.Delete<ProfileAmenitiesResidentialEntity>(delEntity);

                //删除关系表
                string sql = "SELECT * FROM ProfileAmenitiesMainWay_Residential WHERE ResidentialId='" + keyValue + "'";
                db.FindList<ProfileAmenitiesMainWay_ResidentialEntity>(sql).ForEach(d =>
                {
                    db.Delete<ProfileAmenitiesMainWay_ResidentialEntity>(d);
                });


                db.Commit();
            }
        }

        public void SubmitForm(ProfileAmenitiesResidentialEntity Entity, string keyValue, string[] mainWayIds)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {

                if (!string.IsNullOrEmpty(keyValue))//修改
                {
                    //删除之前无用数据
                    db.Update(Entity);
                    string sql = "SELECT * FROM ProfileAmenitiesMainWay_Residential WHERE ResidentialId='" + Entity.F_Id + "'";
                    db.FindList<ProfileAmenitiesMainWay_ResidentialEntity>(sql).ForEach(d =>
                    {
                        db.Delete<ProfileAmenitiesMainWay_ResidentialEntity>(d);
                    });

                    ProfileAmenitiesMainWay_ResidentialEntity centreModle;
                    for (int i = 0; i < mainWayIds.Length; i++)
                    {
                        centreModle = new ProfileAmenitiesMainWay_ResidentialEntity();
                        centreModle.Create();
                        centreModle.MainWayId = mainWayIds[i];
                        centreModle.ResidentialId = Entity.F_Id;
                        db.Insert(centreModle);
                    }
                }
                else
                {
                    db.Insert(Entity);

                    ProfileAmenitiesMainWay_ResidentialEntity centreModle;
                    for (int i = 0; i < mainWayIds.Length; i++)
                    {
                        centreModle = new ProfileAmenitiesMainWay_ResidentialEntity();
                        centreModle.Create();
                        centreModle.MainWayId = mainWayIds[i];
                        centreModle.ResidentialId = Entity.F_Id;
                        db.Insert(centreModle);
                    }
                }
                db.Commit();
            }
        }
    }
}
