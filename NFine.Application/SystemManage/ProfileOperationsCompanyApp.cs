using NFine.Code;
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
    /// 环卫作业公司
    /// </summary>
    public class ProfileOperationsCompanyApp
    {
        private ProfileOperationsCompanyRepository service = new ProfileOperationsCompanyRepository();

        public List<ProfileOperationsCompanyEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString());
        }

        public List<ProfileOperationsCompanyEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ProfileOperationsCompanyEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.CompanyName.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }

        /// <summary>
        /// 获取key value集合 
        /// key:主键
        /// Value:主路名称
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetDictionary(Func<ProfileOperationsCompanyEntity, bool> whereFun)
        {
            return service.dbcontext.Set<ProfileOperationsCompanyEntity>().Where(whereFun).Select(d => new KeyValuePair<string, string>(d.F_Id, d.CompanyName)).ToList();
        }

        //获取数据字典列表
        public List<ProfileOperationsCompanyEntity> GetList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<ProfileOperationsCompanyEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_Id == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.CompanyName.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.F_CreatorTime).ToList();
        }

        public ProfileOperationsCompanyEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void DeleteForm(string keyValue)
        {
            service.Delete(GetForm(keyValue));

            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除作业车辆信息【" + GetForm(keyValue).CompanyName + "】成功！");
            }
            catch { }
        }

        public void SubmitForm(ProfileOperationsCompanyEntity companyEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                companyEntity.Modify(keyValue);
                service.Update(companyEntity);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改作业公司【" + companyEntity.CompanyName + "】成功！");
                }
                catch { }
            }
            else
            {
                companyEntity.Create();
                service.Insert(companyEntity);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "新建作业公司【" + companyEntity.CompanyName + "】成功！");
                }
                catch { }
            }
        }
    }
}
