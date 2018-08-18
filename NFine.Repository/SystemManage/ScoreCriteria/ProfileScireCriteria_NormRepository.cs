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
    /// 评分标准-规范表
    /// </summary>
    public class ProfileScireCriteria_NormRepository : RepositoryBase<ProfileScireCriteria_NormEntity>
    {
        public void SubmitForm(ProfileScireCriteria_NormEntity entry, string normGroupId, string classifyGroupId)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(normGroupId))
                {
                    var normEntitys =db.IQueryable<ProfileScireCriteria_NormEntity>().Where(d => d.GroupId == normGroupId).ToArray();

                    foreach (var item in normEntitys)
                    {
                        item.Condition = entry.Condition;
                        item.SNormProjectName = entry.SNormProjectName;
                        item.SNormStandardName = entry.SNormStandardName;

                        db.Update<ProfileScireCriteria_NormEntity>(item);
                    }
                }
                else
                {
                    string[] classifyIds = db.IQueryable<ProfileScoreCriteria_ClassifyEntity>().Where(d => d.GroupId == classifyGroupId).Select(d => d.SClassifyId).ToArray();

                    ProfileScireCriteria_NormEntity normEntiy;

                    string newNormGroupId = Guid.NewGuid().ToString();

                    foreach (var item in classifyIds)
                    {
                        normEntiy = new ProfileScireCriteria_NormEntity()
                        {
                            Condition = entry.Condition,
                            SNormProjectName = entry.SNormProjectName,
                            SNormStandardName = entry.SNormStandardName,
                            SClassifyId = item,
                            GroupId = newNormGroupId
                        };

                        normEntiy.SNormId = Guid.NewGuid().ToString();

                        db.Insert<ProfileScireCriteria_NormEntity>(normEntiy);
                    }
                }

                db.Commit();
            }
        }

        public void DeleteForm(string groupId)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {

                var deleteEntity = db.IQueryable<ProfileScireCriteria_NormEntity>().Where(d => d.GroupId == groupId).ToArray();

                foreach (var item in deleteEntity)
                {
                    db.Delete<ProfileScireCriteria_NormEntity>(item);
                }

                db.Commit();
            }
        }
    }
}
