using NFine.Domain.Entity.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.SystemManage
{
    /// <summary>
    /// 环评-市容-主路居民小区中间表
    /// </summary>
    public class ProfileAmenitiesMainWay_ResidentialApp
    {
        private ProfileAmenitiesMainWay_ResidentialRepository service = new ProfileAmenitiesMainWay_ResidentialRepository();

        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ProfileAmenitiesMainWay_ResidentialEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString());
        }

        /// <summary>
        /// 根据id获取单挑数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ProfileAmenitiesMainWay_ResidentialEntity GetForm(string keyValue)
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
        }

        /// <summary>
        /// 提交，修改
        /// </summary>
        /// <param name="tandasEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(ProfileAmenitiesMainWay_ResidentialEntity Entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                Entity.Modify(keyValue);

                service.Update(Entity);
            }
            else
            {
                Entity.Create();

                service.Insert(Entity);
            }
        }
    }
}
