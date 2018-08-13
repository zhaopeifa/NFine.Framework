using NFine.Code;
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
    public class ProfileProjectApp
    {
        private ProfileProjectRepository service = new ProfileProjectRepository();

        public List<ProfileProjectEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString());
        }

        public List<entity> FildSql<entity>(Func<ProfileProjectEntity, bool> where,Func<ProfileProjectEntity, entity> select)
        {
            List<entity> result = null;
            service.QueryCommand<ProfileProjectEntity>((query) =>
            {
                result = query.Where(where).Select(select).ToList();
            });

            return result;
        }

        public List<ProfileProjectEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ProfileProjectEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.ProjectName.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }
        //获取数据字典列表
        public List<ProfileProjectEntity> GetList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<ProfileProjectEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.CountyId == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.ProjectName.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.F_CreatorTime).ToList();
        }

        public List<ProfileProjectEntity> GetList(Pagination pagination, string cityId, string countyId, string keyword)
        {
            var expression = ExtLinq.True<ProfileProjectEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.ProjectName.Contains(keyword));
            }
            expression = expression.And(t => t.CityId == countyId && t.CountyId == countyId);
            return service.FindList(expression, pagination);
        }

        public ProfileProjectEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void DeleteForm(string keyValue)
        {
            service.Delete(GetForm(keyValue));

            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除项目信息【" + GetForm(keyValue).ProjectName + "】成功！");
            }
            catch { }
        }

        public void SubmitForm(ProfileProjectEntity productEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                productEntity.Modify(keyValue);
                service.Update(productEntity);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改项目【" + productEntity.ProjectName + "】成功！");
                }
                catch { }
            }
            else
            {
                productEntity.Create();
                service.Insert(productEntity);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "新建项目【" + productEntity.ProjectName + "】成功！");
                }
                catch { }
            }
        }
    }
}
