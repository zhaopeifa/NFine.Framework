using NFine.Code;
using NFine.Data;
using NFine.Data.Extensions;
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
    /// 扣分记录表
    /// </summary>
    public class ProfileDeducInsRepository : RepositoryBase<ProfileDeducInsEntity>
    {

        public void SubmitForm(ProfileDeducInsSubMitContracts entity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                //获取评分标准。
                var scNormEntity = LinqSQLExtensions.IQueryable<ProfileScireCriteria_NormEntity>().Where(d => d.SNormId == entity.NormId).FirstOrDefault();
                var scClassifyEntity = LinqSQLExtensions.IQueryable<ProfileScoreCriteria_ClassifyEntity>().Where(d => d.SClassifyId == scNormEntity.SClassifyId).FirstOrDefault();
                var scTypeEntit = LinqSQLExtensions.IQueryable<ProfileScoreCriteria_TypeEntity>().Where(d => d.STypeId == scClassifyEntity.STypeId).FirstOrDefault();
                var scEntryEntity = LinqSQLExtensions.IQueryable<ProfileScoreCriteria_EntryEntity>().Where(d => d.SEntryId == scTypeEntit.SEntryId).FirstOrDefault();


                //判断当前当中是存在数据，如果存在则是修改
                //两条具体使用哪一个？
                var deducInsQuery = db.IQueryable<ProfileDeducInsEntity>().Where(d => d.DeducIns_Id == keyValue);
                 deducInsQuery = db.IQueryable<ProfileDeducInsEntity>().Where(d => d.TaskEntry_Id == entity.TaskEntryId && d.SCNorm_Id == entity.NormId);

                if (deducInsQuery.Count() > 0)
                {

                    var deducInsEntiy = deducInsQuery.FirstOrDefault();

                    deducInsEntiy.TaskEntry_Id = entity.TaskEntryId;
                    deducInsEntiy.SCNorm_Id = entity.NormId;
                    deducInsEntiy.SCNormProjectName = scNormEntity.SNormProjectName;
                    deducInsEntiy.SCNormStandardName = scNormEntity.SNormStandardName;
                    deducInsEntiy.SCClassifyName = scClassifyEntity.SClassifyName;
                    deducInsEntiy.SCTypeName = scTypeEntit.Name;
                    deducInsEntiy.SCEntryName = scEntryEntity.Name;
                    deducInsEntiy.DeductionScore = entity.DeductionScore;
                    deducInsEntiy.DeductionSeveral = entity.DeductionSeveral;
                    deducInsEntiy.DeductionDescribe = entity.DeductionDescribe;
                    deducInsEntiy.LastModifyTime = DateTime.Now;
                    deducInsEntiy.LastModifyUserId = OperatorProvider.Provider.GetCurrent().UserId;
                    deducInsEntiy.LastModifyUserName = OperatorProvider.Provider.GetCurrent().UserName;


                    db.Update<ProfileDeducInsEntity>(deducInsEntiy);
                }
                else
                {
                    ProfileDeducInsEntity deducInsEntity = new ProfileDeducInsEntity()
                    {
                        DeducIns_Id = keyValue,
                        TaskEntry_Id = entity.TaskEntryId,
                        SCNorm_Id = entity.NormId,
                        SCNormProjectName = scNormEntity.SNormProjectName,
                        SCNormStandardName = scNormEntity.SNormStandardName,
                        SCClassifyName = scClassifyEntity.SClassifyName,
                        SCTypeName = scTypeEntit.Name,
                        SCEntryName = scEntryEntity.Name,
                        DeductionScore = entity.DeductionScore,
                        DeductionSeveral = entity.DeductionSeveral,
                        DeductionDescribe = entity.DeductionDescribe,
                        CreateTime = DateTime.Now,
                        CreatorUserId = OperatorProvider.Provider.GetCurrent().UserId,
                        CreatorUserName = OperatorProvider.Provider.GetCurrent().UserName
                    };

                    db.Insert<ProfileDeducInsEntity>(deducInsEntity);
                }




                db.Commit();
            }
        }
    }
}
