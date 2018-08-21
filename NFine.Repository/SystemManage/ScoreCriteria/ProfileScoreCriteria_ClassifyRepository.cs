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
        public void SubmitForm(ProfileScoreCriteria_ClassifyEntity entry, string groupId, string[] typeIds)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(groupId))
                {
                    //查出之前的小类

                    var classifysEntitys = db.IQueryable<ProfileScoreCriteria_ClassifyEntity>().Where(d => d.GroupId == groupId).ToList();
                    //查找是否存在差异 存在改删除的就删除  改修改的就修改 

                    //要删除的
                    var deleteTypes = classifysEntitys.Select(d => d.STypeId).Except(typeIds).ToList();
                    //新增的
                    var insertTypes = typeIds.Except(classifysEntitys.Select(d => d.STypeId)).ToList();

                    //处理要是删除的集合
                    foreach (var item in deleteTypes)
                    {
                        var classifyEntity = classifysEntitys.FirstOrDefault(d => d.STypeId == item);
                        //查找评分明细的数据
                        var queryNorm = db.IQueryable<ProfileScireCriteria_NormEntity>().Where(d => d.SClassifyId == classifyEntity.SClassifyId);
                        if (queryNorm.Count() > 0)
                        {
                            db.Delete<ProfileScireCriteria_NormEntity>(queryNorm.FirstOrDefault());
                        }
                        //删除当前关联 取消的type（一级道路，特技道路）
                        db.Delete<ProfileScoreCriteria_ClassifyEntity>(classifyEntity);
                        //删除查询数组当中
                        classifysEntitys.Remove(classifyEntity);
                    }


                    //当前关联小类评分评分标准第一项 
                    ProfileScireCriteria_NormEntity fistNormEntity = null;
                    string sClassifyId = classifysEntitys[0].SClassifyId;

                    //查看当前类下是否关联了评分标准
                    var queryInsertNorm = db.IQueryable<ProfileScireCriteria_NormEntity>().Where(d => d.SClassifyId == sClassifyId);
                    if (queryInsertNorm.Count() > 0)
                    {
                        fistNormEntity = queryInsertNorm.FirstOrDefault();
                    }

                    ProfileScoreCriteria_ClassifyEntity insertClassifyEntity = null;
                    ProfileScireCriteria_NormEntity insertNormEntity = null;
                    //处理新增的
                    foreach (var item in insertTypes)
                    {
                        insertClassifyEntity = new ProfileScoreCriteria_ClassifyEntity()
                        {
                            GroupId = classifysEntitys[0].GroupId,
                            SClassifyName = entry.SClassifyName,
                            Score = entry.Score,
                            STypeId = item,
                            SClassifyId = Guid.NewGuid().ToString()
                        };

                        db.Insert<ProfileScoreCriteria_ClassifyEntity>(insertClassifyEntity);


                        //如果当前小类下有关联则每个小类上都关联上
                        if (fistNormEntity != null)
                        {

                            insertNormEntity = new ProfileScireCriteria_NormEntity()
                            {
                                GroupId = fistNormEntity.GroupId,
                                SClassifyId = insertClassifyEntity.SClassifyId,
                                Condition = fistNormEntity.Condition,
                                SNormProjectName = fistNormEntity.SNormProjectName,
                                SNormStandardName = fistNormEntity.SNormStandardName,
                                SNormId = Guid.NewGuid().ToString()
                            };

                            db.Insert<ProfileScireCriteria_NormEntity>(insertNormEntity);
                        }
                    }


                    //修改
                    foreach (var item in classifysEntitys)
                    {
                        item.SClassifyName = entry.SClassifyName;
                        item.Score = entry.Score;

                        db.Update<ProfileScoreCriteria_ClassifyEntity>(item);
                    }

                }
                else
                {
                    ProfileScoreCriteria_ClassifyEntity addModel = null;

                    string newGroupId = Guid.NewGuid().ToString();

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
                            GroupId = newGroupId
                        };

                        db.Insert<ProfileScoreCriteria_ClassifyEntity>(addModel);
                    }
                }

                db.Commit();
            }
        }

        public void DeleteForm(string groupId)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                //删除几个关联的

                var deleteClassifys = db.IQueryable<ProfileScoreCriteria_ClassifyEntity>().Where(d => d.GroupId == groupId).ToArray();

                foreach (var item in deleteClassifys)
                {
                    db.Delete<ProfileScoreCriteria_ClassifyEntity>(item);

                    //删除评分明细
                    var deleteNorms = db.IQueryable<ProfileScireCriteria_NormEntity>().Where(d => d.SClassifyId == item.SClassifyId).ToArray();
                    if (deleteNorms != null &&
                        deleteNorms.Length > 0)
                    {
                        foreach (var delNorm in deleteNorms)
                        {
                            db.Delete<ProfileScireCriteria_NormEntity>(delNorm);
                        }
                    }
                }

                db.Commit();
            }
        }
    }
}
