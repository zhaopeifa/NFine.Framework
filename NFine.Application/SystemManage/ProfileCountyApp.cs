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
    public class ProfileCountyApp
    {
        private ProfileCountyRepository service = new ProfileCountyRepository();
        private ProfileProjectApp projectApp = new ProfileProjectApp();
        private ItemsDetailApp itemsDetailApp = new ItemsDetailApp();

        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ProfileCountyEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取List
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<ProfileCountyEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ProfileCountyEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => !(bool)t.F_DeleteMark);
                expression = expression.And(t => t.CountyName.Contains(keyword));
                expression = expression.Or(t => t.CountyCode.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }
        public List<ProfileCountyEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<ProfileCountyEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.CountyName.Contains(keyword));
                expression = expression.Or(t => t.CountyCode.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.CountyCode).ToList();
        }
        /// <summary>
        /// 根据城市Id获取
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<ProfileCountyEntity> GetList(Pagination pagination, string keyword, string cityId)
        {
            var expression = ExtLinq.True<ProfileCountyEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => !(bool)t.F_DeleteMark);
                expression = expression.And(t => t.CityId == cityId);
                expression = expression.And(t => t.CountyName.Contains(keyword));
                expression = expression.Or(t => t.CountyCode.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }
        /// <summary>
        /// 根据城市Id获取
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<ProfileCountyEntity> GetListByCityId(string cityId)
        {
            var expression = ExtLinq.True<ProfileCountyEntity>();
            expression = expression.And(t => !(bool)t.F_DeleteMark);
            expression = expression.And(t => t.CityId == cityId);
            return service.IQueryable(expression).ToList();
        }

        /// <summary>
        /// 查询、修改、删除用户信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ProfileCountyEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            var udateModel = GetForm(keyValue);
            udateModel.F_DeleteMark = true;
            service.Update(udateModel);
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除区县信息【" + GetForm(keyValue).CountyCode + "】成功！");
            }
            catch { }
        }

        /// <summary>
        /// 修改添加
        /// </summary>
        /// <param name="cityEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(ProfileCountyEntity countyEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                countyEntity.Modify(keyValue);

                string sql = "SELECT * FROM ProfileProject WHERE CityId='" + countyEntity.CityId + "' AND CountyId='" + countyEntity.F_Id + "'";
                var projectData = this.projectApp.FildSql(sql);

                foreach (var item in projectData)
                {
                    item.ProjectName = countyEntity.CountyName + item.ProjectTypeName;

                    this.projectApp.SubmitForm(item, item.F_Id);
                }

                service.Update(countyEntity);

                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改区县信息【" + countyEntity.CountyName + "】成功！");
                }
                catch { }
            }
            else
            {
                countyEntity.Create();

                countyEntity.Create();


                //创建此区县下三个项目
                string sql = "select * from Sys_ItemsDetail,Sys_Items where Sys_Items.F_Id = Sys_ItemsDetail.F_ItemId and Sys_Items.F_EnCode ='ProfileProduct'";
                List<ItemsDetailEntity> ItemsDetailEntityList = itemsDetailApp.FildSql(sql);
                ProfileProjectEntity project = null;
                foreach (var item in ItemsDetailEntityList)
                {
                    project = new ProfileProjectEntity();
                    project.CityId = countyEntity.CityId;
                    project.CountyId = countyEntity.F_Id;
                    project.ProjectType = item.F_ItemCode;
                    project.ProjectTypeName = item.F_ItemName;
                    project.ProjectName = countyEntity.CountyName + item.F_ItemName;

                    this.projectApp.SubmitForm(project, string.Empty);
                }

                service.Insert(countyEntity);

                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "新建区县信息【" + countyEntity.CountyName + "】成功！");
                }
                catch { }

            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="userEntity"></param>
        public void UpdateForm(ProfileCountyEntity countyEntity)
        {
            service.Update(countyEntity);
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改用户信息【" + countyEntity.CountyName + "】成功！");
            }
            catch { }
        }
    }
}
