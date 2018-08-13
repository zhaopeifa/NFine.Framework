using NFine.Data;
using NFine.Domain.Contracts;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.SystemManage
{
    /// <summary>
    /// 环评-环卫-机扫车
    /// </summary>
    public class ProfileSanitationCarRepository : RepositoryBase<ProfileSanitationCarEntity>
    {
        public void SubmitForm(ProfileSanitationCarEntity entity, string keyValue, ProfileCarWorkItemContracts[] works)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {

                if (!string.IsNullOrEmpty(keyValue))//修改
                {
                    db.Update<ProfileSanitationCarEntity>(entity);

                    #region 查看有没有要删除项
                    var workItemDbIds = db.IQueryable<ProfileSanitationCarWorkItemEntity>().Where(d => d.CarId == entity.F_Id).Select(d => d.F_Id).ToArray();
                    var workItemIds = works.Select(d => d.id).ToArray();

                    string[] delworkItems = workItemDbIds.Except(workItemIds).ToArray();

                    foreach (var item in delworkItems)
                    {
                        db.Delete<ProfileSanitationCarWorkItemEntity>(db.FindEntity<ProfileSanitationCarWorkItemEntity>(item));
                    }

                    #endregion


                    ProfileSanitationCarWorkItemEntity workEntity = null;

                    foreach (var item in works)
                    {
                        workEntity = new ProfileSanitationCarWorkItemEntity();

                        workEntity.CarId = entity.F_Id;
                        workEntity.Time = item.time;
                        workEntity.Subscript = item.subscript;
                        workEntity.RinseName = item.rinseName;
                        workEntity.RinseAddress = item.rinseAddress;
                        workEntity.RinseLength = item.rinseLength;

                        if (string.IsNullOrEmpty(item.id) ||
                            item.id.Equals("-1"))
                        {
                            workEntity.Create();

                            db.Insert<ProfileSanitationCarWorkItemEntity>(workEntity);
                        }
                        else
                        {
                            workEntity.F_Id = item.id;

                            workEntity.Modify(item.id);

                            db.Update<ProfileSanitationCarWorkItemEntity>(workEntity);
                        }
                    }
                }
                else
                {
                    db.Insert<ProfileSanitationCarEntity>(entity);

                    ProfileSanitationCarWorkItemEntity workEntity = null;

                    foreach (var item in works)
                    {
                        workEntity = new ProfileSanitationCarWorkItemEntity()
                        {
                            CarId = entity.F_Id,
                            Time = item.time,
                            Subscript = item.subscript,
                            RinseName = item.rinseName,
                            RinseAddress = item.rinseAddress,
                            RinseLength = item.rinseLength
                        };

                        workEntity.Create();

                        db.Insert<ProfileSanitationCarWorkItemEntity>(workEntity);
                    }
                }

                db.Commit();
            }
        }

        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                var delCarEntity =db.FindEntity<ProfileSanitationCarEntity>(keyValue);

                db.Delete<ProfileSanitationCarEntity>(delCarEntity);

                //删除子项
                var delWorks =db.IQueryable<ProfileSanitationCarWorkItemEntity>().Where(d => d.CarId == delCarEntity.F_Id);
                foreach (var item in delWorks)
                {
                    db.Delete<ProfileSanitationCarWorkItemEntity>(item);
                }

                db.Commit();
            }
        }
    }
}
