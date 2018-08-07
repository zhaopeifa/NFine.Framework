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
    /// 环评-市容-水域
    /// </summary>
    public class ProfileAmenitiesWatersRepository : RepositoryBase<ProfileAmenitiesWatersEntity>
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                var delEntity = db.FindEntity<ProfileAmenitiesWatersEntity>(keyValue);
                db.Delete<ProfileAmenitiesWatersEntity>(delEntity);

                //删除关系表
                string sql = "SELECT * FROM ProfileAmenitiesMainWay_Waters WHERE WatersId='" + keyValue + "'";
                db.FindList<ProfileAmenitiesMainWay_WatersEntity>(sql).ForEach(d =>
                {
                    db.Delete<ProfileAmenitiesMainWay_WatersEntity>(d);
                });


                db.Commit();
            }
        }

        public void SubmitForm(ProfileAmenitiesWatersEntity Entity, string keyValue, string[] mainWayIds)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {

                if (!string.IsNullOrEmpty(keyValue))//修改
                {
                    //删除之前无用数据
                    string sql = "SELECT * FROM ProfileAmenitiesMainWay_Waters WHERE WatersId='" + keyValue + "'";
                    db.FindList<ProfileAmenitiesMainWay_WatersEntity>(sql).ForEach(d =>
                    {
                        db.Delete<ProfileAmenitiesMainWay_WatersEntity>(d);
                    });

                    ProfileAmenitiesMainWay_WatersEntity centreModle;
                    for (int i = 0; i < mainWayIds.Length; i++)
                    {
                        centreModle = new ProfileAmenitiesMainWay_WatersEntity();
                        centreModle.Create();
                        centreModle.MainWayId = mainWayIds[i];
                        centreModle.WatersId = Entity.F_Id;
                        db.Insert(centreModle);
                    }
                }
                else
                {
                    db.Insert(Entity);

                    ProfileAmenitiesMainWay_WatersEntity centreModle;
                    for (int i = 0; i < mainWayIds.Length; i++)
                    {
                        centreModle = new ProfileAmenitiesMainWay_WatersEntity();
                        centreModle.Create();
                        centreModle.MainWayId = mainWayIds[i];
                        centreModle.WatersId = Entity.F_Id;
                        db.Insert(centreModle);
                    }
                }
                db.Commit();
            }
        }
    }
}
