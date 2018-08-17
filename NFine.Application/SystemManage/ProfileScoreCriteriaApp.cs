using NFine.Code;
using NFine.Data.Extensions;
using NFine.Domain.Contracts;
using NFine.Domain.Entity.SystemManage;
using NFine.Repository.SystemManage;
using NFine.Web.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.SystemManage
{
    /// <summary>
    /// 评分标准
    /// </summary>
    public class ProfileScoreCriteriaApp
    {
        private ProfileScoreCriteria_EntryRepository entryService = new ProfileScoreCriteria_EntryRepository();
        private ProfileScoreCriteria_TypeRepository typeService = new ProfileScoreCriteria_TypeRepository();

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<ProfileScoreCriteria_EntryEntity> GetEntryList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ProfileScoreCriteria_EntryEntity>();

            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Name.Contains(keyword));
            }

            return entryService.FindList(expression, pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<ScoreCriteriaTypeContracts> GetTypeList(Pagination pagination, string keyword)
        {


            var typeQuery = LinqSQLExtensions.IQueryable<ProfileScoreCriteria_TypeEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                typeQuery = typeQuery.Where(d => d.Name.Contains(keyword));
            }


            typeQuery.Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows);


            var dataQ = from typeQ in typeQuery
                        join entryQ in LinqSQLExtensions.IQueryable<ProfileScoreCriteria_EntryEntity>()
                        on typeQ.SEntryId equals entryQ.SEntryId
                        select new ScoreCriteriaTypeContracts
                        {
                            STypeId = typeQ.STypeId,
                            STypeName = typeQ.Name,
                            SEntryId = typeQ.SEntryId,
                            SEntryName = entryQ.Name
                        };


            return dataQ.ToList();

        }

        /// <summary>
        /// 查询、修改、删除用户信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ProfileScoreCriteria_EntryEntity GetEntryForm(string keyValue)
        {
            return entryService.FindEntity(keyValue);
        }

        /// <summary>
        /// 查询、修改、删除用户信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ProfileScoreCriteria_TypeEntity GetTypeForm(string keyValue)
        {
            return typeService.FindEntity(keyValue);
        }

        /// <summary>
        /// 获取dic
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetDictionary(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);

            return entryService.dbcontext.Database.SqlQuery<ProfileScoreCriteria_EntryEntity>(strSql.ToString()).Select(d => new KeyValuePair<string, string>(d.SEntryId, d.Name)).ToList();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteEntryForm(string keyValue)
        {
            typeService.Delete(GetTypeForm(keyValue));
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除评分标准中信息【" + GetEntryForm(keyValue).Name + "】成功！");
            }
            catch { }
        }

        public void DeleteTypeForm(string keyValue)
        {
            typeService.Delete(GetTypeForm(keyValue));
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除评分标准信息【" + GetTypeForm(keyValue).Name + "】成功！");
            }
            catch { }
        }


        /// <summary>
        /// 修改添加
        /// </summary>
        /// <param name="cityEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitEntryForm(ProfileScoreCriteria_EntryEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.SEntryId = keyValue;
                entryService.Update(entity);
            }
            else
            {
                entity.SEntryId = Guid.NewGuid().ToString();

                entryService.Insert(entity);



            }
        }

        /// <summary>
        /// 修改添加
        /// </summary>
        /// <param name="cityEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitTypeForm(ProfileScoreCriteria_TypeEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.STypeId = keyValue;

                typeService.Update(entity);
            }
            else
            {
                entity.STypeId = Guid.NewGuid().ToString();

                typeService.Insert(entity);



            }
        }
    }
}
