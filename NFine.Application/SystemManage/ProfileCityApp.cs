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
    public class ProfileCityApp
    {
        private ProfileCityRepository service = new ProfileCityRepository();

        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ProfileCityEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取城市列表列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<ProfileCityEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ProfileCityEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => !(bool)t.F_DeleteMark);
                expression = expression.And(t => t.CityName.Contains(keyword));
                expression = expression.Or(t => t.CityCode.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }
        public List<ProfileCityEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<ProfileCityEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.CityName.Contains(keyword));
                expression = expression.Or(t => t.CityCode.Contains(keyword));
            }
            //expression = expression.And(t => !(bool)t.F_DeleteMark);
            return service.IQueryable(expression).OrderBy(t => t.CityCode).ToList();
        }

        /// <summary>
        /// 查询、修改、删除用户信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ProfileCityEntity GetForm(string keyValue)
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
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除城市信息【" + GetForm(keyValue).CityName + "】成功！");
            }
            catch { }
        }

        /// <summary>
        /// 修改添加
        /// </summary>
        /// <param name="cityEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(ProfileCityEntity cityEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                cityEntity.Modify(keyValue);

            }
            else
            {
                cityEntity.Create();

            }

            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改区县信息【" + cityEntity.CityName + "】成功！");
            }
            catch { }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="userEntity"></param>
        public void UpdateForm(ProfileCityEntity cityEntity)
        {
            service.Update(cityEntity);
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改用户信息【" + cityEntity.CityName + "】成功！");
            }
            catch { }
        }
    }
}
