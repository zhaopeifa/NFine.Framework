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
    /// 环评-市容-三年治理计划
    /// </summary>
    public class ProfileAmenitiesGovernRepository : RepositoryBase<ProfileAmenitiesGovernEntity>
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                var delEntity = db.FindEntity<ProfileAmenitiesGovernEntity>(keyValue);
                db.Delete<ProfileAmenitiesGovernEntity>(delEntity);

                //删除关系表
                string sql = "SELECT * FROM ProfileAmenitiesMainWay_Govern WHERE GovernId='" + keyValue + "'";
                db.FindList<ProfileAmenitiesMainWay_GovernEntity>(sql).ForEach(d =>
                {
                    db.Delete<ProfileAmenitiesMainWay_GovernEntity>(d);
                });


                db.Commit();
            }
        }

        public void SubmitForm(ProfileAmenitiesGovernEntity Entity, string keyValue, string[] mainWayIds)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {

                if (!string.IsNullOrEmpty(keyValue))//修改
                {
                    //删除之前无用数据
                    db.Update(Entity);

                    //删除关系表
                    string sql = "SELECT * FROM ProfileAmenitiesMainWay_Govern WHERE GovernId='" + keyValue + "'";
                    db.FindList<ProfileAmenitiesMainWay_GovernEntity>(sql).ForEach(d =>
                    {
                        db.Delete<ProfileAmenitiesMainWay_GovernEntity>(d);
                    });

                    ProfileAmenitiesMainWay_GovernEntity centreModle;
                    for (int i = 0; i < mainWayIds.Length; i++)
                    {
                        centreModle = new ProfileAmenitiesMainWay_GovernEntity();
                        centreModle.Create();
                        centreModle.MainWayId = mainWayIds[i];
                        centreModle.F_CreatorUserId = Entity.F_Id;
                        centreModle.GovernId = Entity.F_Id;
                        db.Insert(centreModle);
                    }
                }
                else
                {
                    db.Insert(Entity);

                    ProfileAmenitiesMainWay_GovernEntity centreModle;
                    for (int i = 0; i < mainWayIds.Length; i++)
                    {
                        centreModle = new ProfileAmenitiesMainWay_GovernEntity();
                        centreModle.Create();
                        centreModle.MainWayId = mainWayIds[i];
                        centreModle.GovernId = Entity.F_Id;
                        db.Insert(centreModle);
                    }
                }
                db.Commit();
            }
        }
    }
}
