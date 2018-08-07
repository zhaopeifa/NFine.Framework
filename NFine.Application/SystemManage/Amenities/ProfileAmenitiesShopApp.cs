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
    /// 环评-市容-一点一档
    /// </summary>
    public class ProfileAmenitiesShopApp
    {
        private ProfileAmenitiesShopRepository service = new ProfileAmenitiesShopRepository();

        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ProfileAmenitiesShopEntity> FildSql(string enCode)
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
        public List<ProfileAmenitiesShopEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ProfileAmenitiesShopEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_EnCode.Contains(keyword));
                expression = expression.Or(t => t.ShopName.Contains(keyword));
                expression = expression.Or(t => t.Address.Contains(keyword));
                expression = expression.Or(t => t.MainWayName.Contains(keyword));
                expression = expression.Or(t => t.RoadOrigin.Contains(keyword));
                expression = expression.Or(t => t.RoadDestination.Contains(keyword));
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
        public List<ProfileAmenitiesShopEntity> GetList(Pagination pagination, string keyword, string projectId, string streetId)
        {
            var expression = ExtLinq.True<ProfileAmenitiesShopEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_EnCode.Contains(keyword));
                expression = expression.Or(t => t.ShopName.Contains(keyword));
                expression = expression.Or(t => t.Address.Contains(keyword));
                expression = expression.Or(t => t.MainWayName.Contains(keyword));
                expression = expression.Or(t => t.RoadOrigin.Contains(keyword));
                expression = expression.Or(t => t.RoadDestination.Contains(keyword));
            }
            if (!string.IsNullOrEmpty(streetId))
            {
                expression = expression.And(t => t.StreetId == streetId);
            }

            expression = expression.And(t => t.ProjectId == projectId);

            return service.FindList(expression, pagination);
        }

        /// <summary>
        /// 根据id获取单挑数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ProfileAmenitiesShopEntity GetForm(string keyValue)
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
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除市容一点一档信息【" + GetForm(keyValue).ShopName + "】成功！");
            }
            catch { }
        }

        /// <summary>
        /// 提交，修改
        /// </summary>
        /// <param name="tandasEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(ProfileAmenitiesShopEntity Entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                Entity.Modify(keyValue);

                service.Update(Entity);

                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改市容一点一档信息【" + Entity.ShopName + "】成功！");
                }
                catch { }
            }
            else
            {
                Entity.Create();

                service.Insert(Entity);

                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "新建市容一点一档信息【" + Entity.ShopName + "】成功！");
                }
                catch { }

            }
        }
    }
}
