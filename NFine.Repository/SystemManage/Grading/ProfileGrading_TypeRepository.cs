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
    public class ProfileGrading_TypeRepository : RepositoryBase<ProfileGrading_TypeEntity>
    {
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="GradeType"></param>
        /// <param name="keyValue"></param>
        /// <param name="relevance"></param>
        /// <param name="options"></param>
        public void SubmitForm(ProfileGrading_TypeEntity entity, int GradeType, string keyValue, string relevance, ProfileGrading_OptionsContracts[] options)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))//修改
                {
                    db.Update<ProfileGrading_TypeEntity>(entity);

                    var typeRlation = db.FindList<ProfileGrading_Type_RlationEntity>("SELECT top 1 * FROM ProfileGrading_Type_Rlation WHERE ProfileGradingTypeId='" + entity.F_Id + "'");

                    if (typeRlation.Count > 0)
                    {
                        typeRlation[0].Modify(typeRlation[0].F_Id);
                        typeRlation[0].ProfileGradeBasicType = GradeType;
                        typeRlation[0].ProfileGradingTypeId = entity.F_Id;
                        typeRlation[0].ProfileGradeType = relevance;

                        db.Update<ProfileGrading_Type_RlationEntity>(typeRlation[0]);
                    }

                    #region 查看有没有要删除的项

                    var optionDbs = db.FindList<ProfileGrading_OptionsEntity>("SELECT * FROM ProfileGrading_Options WHERE ProfileGradingTypeId='" + entity.F_Id + "'");
                    string[] optionDbIds = optionDbs.Select(d => d.F_Id).ToArray();
                    string[] optionIds = options.Where(d => !string.IsNullOrEmpty(d.id)).Select(d => d.id).ToArray();

                    string[] deloption = optionDbIds.Except(optionIds).ToArray();

                    for (int i = 0; i < deloption.Length; i++)
                    {
                        var delModel = optionDbs.SingleOrDefault(d => d.F_Id == deloption[i]);
                        db.Delete<ProfileGrading_OptionsEntity>(delModel);

                        var normDbs = db.FindList<ProfileGrading_NormEntity>("SELECT * FROM dbo.ProfileGrading_Norm WHERE OptionsId='" + delModel.F_Id + "'");
                        for (int j = 0; j < normDbs.Count; j++)
                        {
                            db.Delete<ProfileGrading_NormEntity>(normDbs[i]);
                        }
                    }

                    #endregion

                    ProfileGrading_OptionsEntity gradingOption = null;
                    for (int i = 0; i < options.Length; i++)
                    {
                        gradingOption = new ProfileGrading_OptionsEntity();
                        gradingOption.Name = options[i].name;
                        gradingOption.ProfileGradingTypeId = entity.F_Id;

                        if (string.IsNullOrEmpty(options[i].id))
                        {
                            gradingOption.Create();

                            db.Insert<ProfileGrading_OptionsEntity>(gradingOption);
                        }
                        else
                        {
                            gradingOption.F_Id = options[i].id;

                            gradingOption.Modify(options[i].id);

                            db.Update<ProfileGrading_OptionsEntity>(gradingOption);
                        }
                    }

                    db.Commit();
                }
                else
                {
                    db.Insert<ProfileGrading_TypeEntity>(entity);

                    //添加关联表
                    var typeRlation = new ProfileGrading_Type_RlationEntity()
                    {
                        ProfileGradeBasicType = GradeType,
                        ProfileGradingTypeId = entity.F_Id,
                        ProfileGradeType = relevance,
                    };
                    typeRlation.Create();
                    db.Insert<ProfileGrading_Type_RlationEntity>(typeRlation);

                    //当前下检查项目
                    ProfileGrading_OptionsEntity gradingOption = null;
                    for (int i = 0; i < options.Length; i++)
                    {
                        gradingOption = new ProfileGrading_OptionsEntity();
                        gradingOption.Name = options[i].name;
                        gradingOption.ProfileGradingTypeId = entity.F_Id;

                        gradingOption.Create();

                        db.Insert<ProfileGrading_OptionsEntity>(gradingOption);
                    }

                    db.Commit();
                }
            }
        }

        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {

                var delEntity = db.FindEntity<ProfileGrading_TypeEntity>(keyValue);
                db.Delete<ProfileGrading_TypeEntity>(delEntity);

                //删除关联

                var typeRlation = db.FindList<ProfileGrading_Type_RlationEntity>("SELECT top 1 * FROM ProfileGrading_Type_Rlation WHERE ProfileGradingTypeId='" + keyValue + "'");
                if (typeRlation.Count > 0)
                {
                    db.Delete<ProfileGrading_Type_RlationEntity>(typeRlation[0]);
                }

                //删除评分项
                var optionDbs = db.FindList<ProfileGrading_OptionsEntity>("SELECT * FROM ProfileGrading_Options WHERE ProfileGradingTypeId='" + keyValue + "'");
                for (int i = 0; i < optionDbs.Count; i++)
                {
                    db.Delete<ProfileGrading_OptionsEntity>(optionDbs[i]);

                    //删除评分项下面的子项
                    var normDbs = db.FindList<ProfileGrading_NormEntity>("SELECT * FROM dbo.ProfileGrading_Norm WHERE OptionsId='" + optionDbs[i].F_Id + "'");
                    for (int j = 0; j < normDbs.Count; j++)
                    {
                        db.Delete<ProfileGrading_NormEntity>(normDbs[i]);
                    }
                }

                db.Commit();
            }
        }
    }
}
