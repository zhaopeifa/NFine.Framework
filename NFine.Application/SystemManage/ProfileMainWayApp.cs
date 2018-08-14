using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Repository.SystemManage;
using NFine.Web.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace NFine.Application.SystemManage
{
    public class ProfileMainWayApp
    {
        private ProfileMainWayRespository service = new ProfileMainWayRespository();

        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ProfileMainWayEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pagination">分页，排序参数</param>
        /// <param name="keyword">检索关键字</param>
        /// <returns></returns>
        public List<ProfileMainWayEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ProfileMainWayEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_EnCode.Contains(keyword));
                expression = expression.Or(t => t.MainWayName.Contains(keyword));
            }

            return service.FindList(expression, pagination);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pagination">分页，排序参数</param>
        /// <param name="keyword">检索关键字</param>
        /// <param name="projectId">关联项目Id</param>
        /// <returns></returns>
        public List<ProfileMainWayEntity> GetList(Pagination pagination, string keyword, string countyId)
        {
            var expression = ExtLinq.True<ProfileMainWayEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.MainWayName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }

            expression = expression.And(t => t.CountyId == countyId);

            return service.FindList(expression, pagination);
        }

        /// <summary>
        /// 获取key value集合 
        /// key:主键
        /// Value:主路名称
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetDictionary(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);

            return service.dbcontext.Database.SqlQuery<ProfileMainWayEntity>(strSql.ToString()).Select(d => new KeyValuePair<string, string>(d.F_Id, d.MainWayName)).ToList();
        }

        /// <summary>
        /// 获取key value集合 
        /// key:主键
        /// Value:主路名称
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetDictionary(Func<ProfileMainWayEntity, bool> whereFun)
        {
            return service.dbcontext.Set<ProfileMainWayEntity>().Where(whereFun).Select(d => new KeyValuePair<string, string>(d.F_Id, d.MainWayName)).ToList();
        }

        /// <summary>
        /// 根据id获取单挑数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ProfileMainWayEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            service.Delete(GetForm(keyValue));
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除环卫主路信息【" + GetForm(keyValue).MainWayName + "】成功！");
            }
            catch { }
        }

        /// <summary>
        /// 提交，修改
        /// </summary>
        /// <param name="tandasEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(ProfileMainWayEntity mainWayEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                mainWayEntity.Modify(keyValue);

                service.Update(mainWayEntity);

                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改环卫主路信息【" + mainWayEntity.MainWayName + "】成功！");
                }
                catch { }
            }
            else
            {
                mainWayEntity.Create();

                service.Insert(mainWayEntity);

                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "新建环卫主路信息【" + mainWayEntity.MainWayName + "】成功！");
                }
                catch { }

            }
        }

        /// <summary>
        /// 数据导入
        /// </summary>
        public void ImportData(ProfileMainWayEntity[] mainWayEntitys, out int successfulCount, out int failureCount)
        {
            successfulCount = 0;
            failureCount = 0;
            for (int i = 0; i < mainWayEntitys.Length; i++)
            {
                if (mainWayEntitys[i] == null)
                    continue;

                mainWayEntitys[i].Create();

                try
                {
                    service.Insert(mainWayEntitys[i]);
                    successfulCount += 1;
                }
                catch
                {
                    failureCount += 1;
                }
            }
        }

        public string GetId(string wayName)
        {
            var query =NFine.Data.Extensions.LinqSQLExtensions.IQueryable<ProfileMainWayEntity>().Where(d => d.MainWayName == wayName).Select(d => d.F_Id);
            if (query.Count() > 0)
                return query.FirstOrDefault();
            return null;
        }
    }
}
