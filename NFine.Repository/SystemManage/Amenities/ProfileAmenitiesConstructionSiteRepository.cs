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

                //删除关系表
                string sql = "SELECT * FROM ProfileAmenitiesMainWay_Construction WHERE ConstructionlId='" + keyValue + "'";
                db.FindList<ProfileAmenitiesMainWay_ConstructionEntity>(sql).ForEach(d =>
                {
                    db.Delete<ProfileAmenitiesMainWay_ConstructionEntity>(d);
                });


                db.Commit();
            }
        }

        public void SubmitForm(ProfileAmenitiesConstructionSiteEntity Entity, string keyValue, string[] mainWayIds)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {

                if (!string.IsNullOrEmpty(keyValue))//修改
                {
                    //删除之前无用数据
                    db.Update(Entity);
                    string sql = "SELECT * FROM ProfileAmenitiesMainWay_Construction WHERE ConstructionlId='" + Entity.F_Id + "'";
                    db.FindList<ProfileAmenitiesMainWay_ConstructionEntity>(sql).ForEach(d =>
                    {
                        db.Delete<ProfileAmenitiesMainWay_ConstructionEntity>(d);
                    });

                    ProfileAmenitiesMainWay_ConstructionEntity centreModle;
                    for (int i = 0; i < mainWayIds.Length; i++)
                    {
                        centreModle = new ProfileAmenitiesMainWay_ConstructionEntity();
                        centreModle.Create();
                        centreModle.MainWayId = mainWayIds[i];
                        centreModle.ConstructionlId = Entity.F_Id;
                        db.Insert(centreModle);
                    }
                }
                else
                {
                    db.Insert(Entity);

                    ProfileAmenitiesMainWay_ConstructionEntity centreModle;
                    for (int i = 0; i < mainWayIds.Length; i++)
                    {
                        centreModle = new ProfileAmenitiesMainWay_ConstructionEntity();
                        centreModle.Create();
                        centreModle.MainWayId = mainWayIds[i];
                        centreModle.ConstructionlId = Entity.F_Id;
                        db.Insert(centreModle);
                    }
                }
                db.Commit();
            }
        }
    }
}
