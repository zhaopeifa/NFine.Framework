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
    /// 评分标准小类
    /// </summary>
    public class ProfileScoreCriteria_ClassifyRepository : RepositoryBase<ProfileScoreCriteria_ClassifyEntity>
    {
        public void SubmitForm(ProfileScoreCriteria_ClassifyEntity entry, string keyValue, string[] typeIds)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {

                }
                else
                {
                    ProfileScoreCriteria_ClassifyEntity addModel = null;

                    string groupId = Guid.NewGuid().ToString();

                    foreach (var item in typeIds)
                    {
                        addModel = new ProfileScoreCriteria_ClassifyEntity();


                        //每一个classify只能对应一个Tyoeids 添加多个
                        addModel = new ProfileScoreCriteria_ClassifyEntity()
                        {
                            SClassifyId = Guid.NewGuid().ToString(),
                            SClassifyName = entry.SClassifyName,
                            Score = entry.Score,
                            STypeId = item,
                            GroupId = groupId
                        };

                        db.Insert<ProfileScoreCriteria_ClassifyEntity>(addModel);
                    }
                }

                db.Commit();
            }
        }
    }
}
