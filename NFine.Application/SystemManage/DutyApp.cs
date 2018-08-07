/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFine.Web.Function;
namespace NFine.Application.SystemManage
{
    public class DutyApp
    {
        private IRoleRepository service = new RoleRepository();


        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<RoleEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString()).ToList();
        }

        //获取岗位列表
        public List<RoleEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<RoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.F_Category == 2);
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        
        //获取启用的岗位列表
        public List<RoleEntity> GetEnableList(string keyword = "")
        {
            var expression = ExtLinq.True<RoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.F_Category == 2);
            expression = expression.And(t => t.F_EnabledMark == true);
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }


        //获取、删除、修改岗位信息
        public RoleEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void DeleteForm(string keyValue)
        {
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除岗位信息【" + GetForm(keyValue).F_FullName + "】成功！");
            }
            catch { }
            service.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(RoleEntity roleEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.Modify(keyValue);
                service.Update(roleEntity);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改岗位信息【" + roleEntity.F_FullName + "】成功！");
                }
                catch { }
            }
            else
            {
                roleEntity.Create();
                roleEntity.F_Category = 2;
                service.Insert(roleEntity);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "新建岗位信息【" + roleEntity.F_FullName + "】成功！");
                }
                catch { }
            }
        }
    }
}
