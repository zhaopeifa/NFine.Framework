using NFine.Code;
using NFine.Domain.Contracts;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.Enums;
using NFine.Repository.SystemManage;
using NFine.Web.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.SystemManage
{
    public class ScoringCriteriaApp
    {
        private ProfileGrading_TypeRepository GType = new ProfileGrading_TypeRepository();
        /// <summary>
        /// 关联
        /// </summary>
        private ProfileGrading_Type_RlationRepository GTypeR = new ProfileGrading_Type_RlationRepository();
        /// <summary>
        /// 检查项目
        /// </summary>
        private ProfileGrading_OptionsRepository GOptions = new ProfileGrading_OptionsRepository();
        /// <summary>
        /// 评分标准
        /// </summary>
        private ProfileGrading_NormRepository GNorms = new ProfileGrading_NormRepository();

        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ProfileGrading_TypeEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return GType.FindList(strSql.ToString()).ToList();
        }

        /// <summary>
        /// 获取评分标准关联
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ProfileGrading_Type_RlationEntity> TypeRlationFildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return GTypeR.FindList(strSql.ToString()).ToList();
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public List<ProfileGrading_OptionsEntity> OptionsFindSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return GOptions.FindList(strSql.ToString()).ToList();
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pagination">分页，排序参数</param>
        /// <param name="keyword">检索关键字</param>
        /// <returns></returns>
        public List<ProfileGrading_TypeContracts> GetList(Pagination pagination, string keyword, ProfileGradeBasicDataEnum type)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("SELECT * FROM ProfileGrading_Type_Rlation WHERE ProfileGradeBasicType=" + (int)type);

            KeyValuePair<string, string>[] grading_TypeRlationList = GTypeR.dbcontext.Database.SqlQuery<ProfileGrading_Type_RlationEntity>(strsql.ToString()).Select(d => new KeyValuePair<string, string>(d.ProfileGradingTypeId, d.ProfileGradeType)).ToArray();

            var expression = ExtLinq.True<ProfileGrading_TypeEntity>();

            for (int i = 0; i < grading_TypeRlationList.Length; i++)
            {
                var idWhere = grading_TypeRlationList[i].Key.ToString();
                if (i == 0)
                {
                    expression = expression.And(t => t.F_Id.Contains(idWhere));
                }
                else
                {
                    expression = expression.Or(t => t.F_Id.Contains(idWhere));
                }
            }
            if (grading_TypeRlationList.Length <= 0)
            {
                expression = expression = expression.And(t => t.F_Id == "-1");
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Name.Contains(keyword));
            }

            return GType.FindList(expression, pagination).Select(d => new ProfileGrading_TypeContracts()
            {
                F_Id = d.F_Id,
                Name = d.Name,
                Grade = d.Grade,
                AssociatedClassifyStr = grading_TypeRlationList.SingleOrDefault(f => f.Key == d.F_Id).Value
            }).ToList();
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pagination">分页，排序参数</param>
        /// <param name="keyword">检索关键字</param>
        /// <returns></returns>
        public List<ProfileGrading_NormEntity> GetNormList(Pagination pagination, string optionsId, string keyword)
        {
            var expression = ExtLinq.True<ProfileGrading_NormEntity>();

            expression = expression.And(t => t.OptionsId == optionsId);

            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Describe.Contains(keyword));
            }

            return GNorms.FindList(expression, pagination);
        }

        /// <summary>
        /// 添加修改
        /// </summary>
        /// <param name="Entity"></param>
        /// <param name="keyValue"></param>
        /// <param name="Relevance"></param>
        /// <param name="Options"></param>
        public void SubmitForm(ProfileGrading_TypeEntity Entity, int GradeType, string keyValue, string Relevance, ProfileGrading_OptionsContracts[] Options)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                Entity.Modify(keyValue);
            }
            else
            {
                Entity.Create();
            }

            GType.SubmitForm(Entity, GradeType, keyValue, Relevance, Options);

            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "新建" + (ProfileGradeBasicDataEnum)GradeType + "评分标准类信息【" + Entity.Name + "】成功！");
            }
            catch { }
        }

        /// <summary>
        /// 添加修改 评分标准
        /// </summary>
        /// <param name="Entity"></param>
        /// <param name="keyValue"></param>
        /// <param name="Relevance"></param>
        /// <param name="Options"></param>
        public void SubmitNormForm(ProfileGrading_NormEntity Entity,string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                Entity.Modify(keyValue);

                this.GNorms.Update(Entity);

                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "新建成功", "新建评分标准信息【" + Entity.Describe + "】成功！");
                }
                catch { }
            }
            else
            {
                Entity.Create();

                this.GNorms.Insert(Entity);

                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "新建评分标准信息【" + Entity.Describe + "】成功！");
                }
                catch { }
            }
        }

        /// <summary>
        /// 根据id获取单挑数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ProfileGrading_TypeEntity GetForm(string keyValue)
        {
            return GType.FindEntity(keyValue);
        }

        public ProfileGrading_NormEntity GetNormForm(string keyValue)
        {
            return GNorms.FindEntity(keyValue);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            GType.DeleteForm(keyValue);
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除评分标准信息【" + GetForm(keyValue).Name + "】成功！");
            }
            catch { }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteNormForm(string keyValue)
        {
            GNorms.Delete(GetNormForm(keyValue));
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除评分标准信息【" + GetNormForm(keyValue).Describe + "】成功！");
            }
            catch { }
        }
    }
}
