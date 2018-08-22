using NFine.Code;
using NFine.Data.Extensions;
using NFine.Domain.Contracts;
using NFine.Domain.Entity.SystemManage;
using NFine.Repository.SystemManage;
using NFine.Web.Function;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        private ProfileScoreCriteria_ClassifyRepository classApp = new ProfileScoreCriteria_ClassifyRepository();
        private ProfileScireCriteria_NormRepository normApp = new ProfileScireCriteria_NormRepository();

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


            return dataQ.OrderBy(d=>d.SEntryId).ToList();

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
        public List<KeyValuePair<string, string>> GetEntryDictionary(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);

            return entryService.dbcontext.Database.SqlQuery<ProfileScoreCriteria_EntryEntity>(strSql.ToString()).Select(d => new KeyValuePair<string, string>(d.SEntryId, d.Name)).ToList();
        }

        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetTypeDictionary(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);

            return typeService.dbcontext.Database.SqlQuery<ProfileScoreCriteria_TypeEntity>(strSql.ToString()).Select(d => new KeyValuePair<string, string>(d.STypeId, d.Name)).ToList();
        }

        public List<KeyValuePair<string, string>> GetClassifyDictionary(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);

            return typeService.dbcontext.Database.SqlQuery<ProfileScoreCriteria_ClassifyEntity>(strSql.ToString()).Select(d => new KeyValuePair<string, string>(d.SClassifyId, d.SClassifyName)).ToList();
        }

        public List<KeyValuePair<string, string>> GetClassifyGroupDictionary(string sEntryId)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>(); ;

            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"SELECT GroupId,SClassifyName FROM ProfileScoreCriteria_Classify WHERE SClassifyId IN(
                            SELECT a.SClassifyId FROM ProfileScoreCriteria_Classify a LEFT  JOIN ProfileScoreCriteria_Type b
                            ON a.STypeId=b.STypeId LEFT JOIN ProfileScoreCriteria_Entry c
                            ON b.SEntryId=c.SEntryId WHERE b.SEntryId='" + sEntryId + "') GROUP BY GroupId,SClassifyName");


            DataTable table = DbHelper.ExecuteDataTable(strSql.ToString(), null);

            if (table == null)
            {
                return result;
            }

            string value = string.Empty;
            string key = string.Empty;
            for (int i = 0; i < table.Rows.Count; i++)
            {


                if (table.Rows[i]["GroupId"] != null)
                {
                    key = table.Rows[i]["GroupId"].ToString();
                }
                if (table.Rows[i]["SClassifyName"] != null)
                {
                    value = table.Rows[i]["SClassifyName"].ToString();
                }

                result.Add(new KeyValuePair<string, string>(key, value));
            }

            return result;
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public DataTable GetClassifyDataTable(Pagination pagination, string keyword)
        {

            StringBuilder sqlStr = new StringBuilder();

            sqlStr.Append(@"SELECT c.Name AS SEntryName,b.Name AS STypeName,a.SClassifyId as SClassifyId,a.SClassifyName as SClassifyName,a.Score as Score FROM ProfileScoreCriteria_Classify a LEFT JOIN ProfileScoreCriteria_Type b
                             ON a.STypeId=b.STypeId LEFT JOIN ProfileScoreCriteria_Entry c 
                             ON b.SEntryId=c.SEntryId where 1=1 ");

            if (!string.IsNullOrEmpty(keyword))
            {
                sqlStr.Append(" and a.SClassifyName like '%" + keyword + "'% ");
                sqlStr.Append(" or b.Name like '" + keyword + "' ");
                sqlStr.Append(" or c.Name like '" + keyword + "' ");
            }

            sqlStr.Append(" ORDER BY a.GroupId ");
            sqlStr.Append(" offset " + pagination.rows * (pagination.page - 1) + " rows fetch next " + pagination.rows * (pagination.page - 1) + pagination.rows + " rows ONLY");



            DataTable table = DbHelper.ExecuteDataTable(sqlStr.ToString(), null);

            if (table != null)
            {
                pagination.records = table.Rows.Count;
            }

            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<ScorCriteriaClassifyContracts> GetClassifyDataTable1(Pagination pagination, string keyword)
        {
            List<ScorCriteriaClassifyContracts> result = new List<ScorCriteriaClassifyContracts>();

            StringBuilder sqlStr = new StringBuilder();

            sqlStr.Append("SELECT GroupId,SClassifyName,Score ");
            sqlStr.Append("FROM ProfileScoreCriteria_Classify ");
            sqlStr.Append("GROUP BY GroupId,SClassifyName,Score ");

            DataTable table = DbHelper.ExecuteDataTable(sqlStr.ToString(), null);

            if (table == null)
            {
                return null;
            }

            ScorCriteriaClassifyContracts entiry = null;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                entiry = new ScorCriteriaClassifyContracts();
                if (table.Rows[i]["GroupId"] != null)
                {
                    entiry.groupId = table.Rows[i]["GroupId"].ToString();
                }
                if (table.Rows[i]["SClassifyName"] != null)
                {
                    entiry.SClassifyName = table.Rows[i]["SClassifyName"].ToString();
                }
                if (table.Rows[i]["Score"] != null)
                {
                    entiry.Score = int.Parse(table.Rows[i]["Score"].ToString());
                }

                sqlStr.Clear();

                sqlStr.Append(@"SELECT STypeId,a.Name AS typeName,b.Name AS EntryName FROM ProfileScoreCriteria_Type a JOIN ProfileScoreCriteria_Entry b
                                ON a.SEntryId=b.SEntryId
                                WHERE STypeId IN
                                (SELECT STypeId FROM ProfileScoreCriteria_Classify 
                                WHERE GroupId='" + entiry.groupId + "') ");

                var typesTable = DbHelper.ExecuteDataTable(sqlStr.ToString(), null);

                result.Add(entiry);

                if (typesTable == null || typesTable.Rows.Count <= 0)
                    continue;

                string[] typeIds = new string[typesTable.Rows.Count];
                string[] typeNames = new string[typesTable.Rows.Count];
                for (int j = 0; j < typesTable.Rows.Count; j++)
                {
                    typeIds[j] = typesTable.Rows[j]["STypeId"].ToString();
                    typeNames[j] = typesTable.Rows[j]["typeName"].ToString();
                }

                entiry.STypeIds = typeIds;
                entiry.STypeNames = typeNames;
                entiry.EntryName = typesTable.Rows[0]["EntryName"].ToString();

            }

            return result;
        }


        public void DeleteClassifyForm(string classifyGroupId)
        {
            classApp.DeleteForm(classifyGroupId);
        }

        /// <summary>
        /// 查找小类
        /// </summary>
        /// <param name="classifyGroupId"></param>
        /// <returns></returns>
        public ProfileScoreCriteria_ClassifyEntity GetClassifyFrom(string classifyGroupId)
        {
            var query = LinqSQLExtensions.IQueryable<ProfileScoreCriteria_ClassifyEntity>().Where(d => d.GroupId == classifyGroupId);
            if (query.Count() > 0)
            {
                return query.FirstOrDefault();
            }
            return null;
        }

        /// <summary>
        /// 获取小类管理的所有中类
        /// </summary>
        /// <returns></returns>
        public List<string> GetClassifyAssociatedType(string classifyGroupId)
        {
            var query = LinqSQLExtensions.IQueryable<ProfileScoreCriteria_ClassifyEntity>().Where(d => d.GroupId == classifyGroupId);

            return query.Select(d => d.STypeId).ToList();
        }


        /// <summary>
        ///获取评分标准 暂未写完
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public DataTable GetNormTable(Pagination pagination, string keyword, string groupId)
        {
            StringBuilder sqlStr = new StringBuilder();

            sqlStr.Append(@"SELECT GroupId,SNormProjectName,Condition,SNormStandardName FROM ProfileScireCriteria_Norm WHERE SClassifyId IN");
            sqlStr.Append("(");
            sqlStr.Append(" SELECT SClassifyId  FROM ProfileScoreCriteria_Classify WHERE GroupId='" + groupId + "'");
            sqlStr.Append(") ");

            if (!string.IsNullOrEmpty(keyword))
            {
                sqlStr.Append(" and SNormProjectName like '%" + keyword + "%' ");
                sqlStr.Append(" or SNormProjectName like '%" + keyword + "%' ");
            }

            sqlStr.Append("GROUP BY GroupId,SNormProjectName,Condition,SNormStandardName ");

            sqlStr.Append(" ORDER BY SNormProjectName ");
            sqlStr.Append(" offset " + pagination.rows * (pagination.page - 1) + " rows fetch next " + pagination.rows * (pagination.page - 1) + pagination.rows + " rows ONLY");

            DataTable table = DbHelper.ExecuteDataTable(sqlStr.ToString(), null);

            return table;
        }

        public ProfileScireCriteria_NormEntity GetNormForm(string normGroupId)
        {
            var query = LinqSQLExtensions.IQueryable<ProfileScireCriteria_NormEntity>().Where(d => d.GroupId == normGroupId);
            if (query.Count() > 0)
            {
                return query.FirstOrDefault();
            }
            return null;
        }

        public DataTable GetClassifyGroupBy(string strWhere)
        {
            StringBuilder sqlStr = new StringBuilder();

            sqlStr.Append("SELECT GroupId,SClassifyName FROM ProfileScoreCriteria_Classify where 1=1 ");

            if (!string.IsNullOrEmpty(strWhere))
            {
                sqlStr.Append(" and " + strWhere);
            }

            return DbHelper.ExecuteDataTable(sqlStr.ToString(), null);
        }


        public void SubmitClassifyForm(ProfileScoreCriteria_ClassifyEntity entry, string keyValue, string[] typeIds)
        {
            classApp.SubmitForm(entry, keyValue, typeIds);
        }

        public void SubmitNormForm(ProfileScireCriteria_NormEntity entry, string normGroupId, string classifyGroupId)
        {
            normApp.SubmitForm(entry, normGroupId, classifyGroupId);
        }

        public void DeleteNormForm(string groupId)
        {
            normApp.DeleteForm(groupId);
        }
    }
}
