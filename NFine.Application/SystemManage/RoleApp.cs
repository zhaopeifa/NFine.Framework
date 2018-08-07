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
    public class RoleApp
    {
        private IRoleRepository service = new RoleRepository();
        private ModuleApp moduleApp = new ModuleApp();
        private ModuleButtonApp moduleButtonApp = new ModuleButtonApp();

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

        //获取启用角色列表
        public List<RoleEntity> GetEnableList(string keyword = "")
        {
            var expression = ExtLinq.True<RoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.F_Category == 1);
            expression = expression.And(t => t.F_EnabledMark == true);
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        //获取角色列表
        public List<RoleEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<RoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.F_Category == 1);
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        //获取、删除、修改信息
        public RoleEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除角色信息【" + GetForm(keyValue).F_FullName + "】成功！");
            }
            catch { }
        }
        public void SubmitForm(RoleEntity roleEntity, string[] permissionIds, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.F_Id = keyValue;
            }
            else
            {
                roleEntity.F_Id = Common.GuId();
            }
            var moduledata = moduleApp.GetList();
            var buttondata = moduleButtonApp.GetList();
            List<RoleAuthorizeEntity> roleAuthorizeEntitys = new List<RoleAuthorizeEntity>();
            foreach (var itemId in permissionIds)
            {
                RoleAuthorizeEntity roleAuthorizeEntity = new RoleAuthorizeEntity();
                roleAuthorizeEntity.F_Id = Common.GuId();
                roleAuthorizeEntity.F_ObjectType = 1;
                roleAuthorizeEntity.F_ObjectId = roleEntity.F_Id;
                roleAuthorizeEntity.F_ItemId = itemId;
                if (moduledata.Find(t => t.F_Id == itemId) != null)
                {
                    roleAuthorizeEntity.F_ItemType = 1;
                }
                if (buttondata.Find(t => t.F_Id == itemId) != null)
                {
                    roleAuthorizeEntity.F_ItemType = 2;
                }
                roleAuthorizeEntitys.Add(roleAuthorizeEntity);
            }
            service.SubmitForm(roleEntity, roleAuthorizeEntitys, keyValue);

            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改角色信息【" + roleEntity.F_FullName + "】成功！");
            }
            catch { }
        }
    }
}
