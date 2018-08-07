/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Domain.ViewModel;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFine.Web.Function;
namespace NFine.Application.SystemManage
{
    public class RoleAuthorizeApp
    {
        private IRoleAuthorizeRepository service = new RoleAuthorizeRepository();
        private ModuleApp moduleApp = new ModuleApp();
        private ModuleButtonApp moduleButtonApp = new ModuleButtonApp();
        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<RoleAuthorizeEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString()).ToList();
        }

        //获取角色权限列表
        public List<RoleAuthorizeEntity> GetList(string ObjectId)
        {
            return service.IQueryable(t => t.F_ObjectId == ObjectId).ToList();
        }

        //获取角色菜单权限列表
        public List<ModuleEntity> GetMenuList(string roleId)
        {
            var data = new List<ModuleEntity>();
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                data = moduleApp.GetList();
            }
            else
            {
                var moduledata = moduleApp.GetList();
                var authorizedata = service.IQueryable(t => t.F_ObjectId == roleId && t.F_ItemType == 1).ToList();
                foreach (var item in authorizedata)
                {
                    ModuleEntity moduleEntity = moduledata.Find(t => t.F_Id == item.F_ItemId);
                    if (moduleEntity != null)
                    {
                        data.Add(moduleEntity);
                    }
                }
            }
            return data.OrderBy(t => t.F_SortCode).ToList();
        }


        //获取角色启用的菜单权限列表
        public List<ModuleEntity> GetEnableMenuList(string roleId)
        {
            var data = new List<ModuleEntity>();
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                data = moduleApp.GetEnabledList();
            }
            else
            {
                var moduledata = moduleApp.GetEnabledList();
                var authorizedata = service.IQueryable(t => t.F_ObjectId == roleId && t.F_ItemType == 1).ToList();
                foreach (var item in authorizedata)
                {
                    ModuleEntity moduleEntity = moduledata.Find(t => t.F_Id == item.F_ItemId);
                    if (moduleEntity != null)
                    {
                        data.Add(moduleEntity);
                    }
                }
            }
            return data.OrderBy(t => t.F_SortCode).ToList();
        }


        //获取角色按钮权限列表

        public List<ModuleButtonEntity> GetButtonList(string roleId)
        {
            var data = new List<ModuleButtonEntity>();
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                data = moduleButtonApp.GetList();
            }
            else
            {
                var buttondata = moduleButtonApp.GetList();
                var authorizedata = service.IQueryable(t => t.F_ObjectId == roleId && t.F_ItemType == 2).ToList();
                foreach (var item in authorizedata)
                {
                    ModuleButtonEntity moduleButtonEntity = buttondata.Find(t => t.F_Id == item.F_ItemId);
                    if (moduleButtonEntity != null)
                    {
                        data.Add(moduleButtonEntity);
                    }
                }
            }
            return data.OrderBy(t => t.F_SortCode).ToList();
        }

        //获取角色、菜单中按钮权限列表
        public List<ModuleButtonEntity> GetButtonList(string roleId,string menu_id)
        {
            var data = new List<ModuleButtonEntity>();
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                data = moduleButtonApp.GetList(menu_id);
            }
            else
            {
                var buttondata = moduleButtonApp.GetList(menu_id);
                var authorizedata = service.IQueryable(t => t.F_ObjectId == roleId && t.F_ItemType == 2).ToList();
                foreach (var item in authorizedata)
                {
                    ModuleButtonEntity moduleButtonEntity = buttondata.Find(t => t.F_Id == item.F_ItemId);
                    if (moduleButtonEntity != null)
                    {
                        data.Add(moduleButtonEntity);
                    }
                }
            }
            return data.OrderBy(t => t.F_SortCode).ToList();
        }

        //提交验证
        public bool ActionValidate(string roleId, string moduleId, string action)
        {
            var authorizeurldata = new List<AuthorizeActionModel>();
            var cachedata = CacheFactory.Cache().GetCache<List<AuthorizeActionModel>>("authorizeurldata_" + roleId);
            if (cachedata == null)
            {
                var moduledata = moduleApp.GetList();
                var buttondata = moduleButtonApp.GetList();
                var authorizedata = service.IQueryable(t => t.F_ObjectId == roleId).ToList();
                foreach (var item in authorizedata)
                {
                    if (item.F_ItemType == 1)
                    {
                        ModuleEntity moduleEntity = moduledata.Find(t => t.F_Id == item.F_ItemId);
                        authorizeurldata.Add(new AuthorizeActionModel { F_Id = moduleEntity.F_Id, F_UrlAddress = moduleEntity.F_UrlAddress });
                    }
                    else if (item.F_ItemType == 2)
                    {
                        ModuleButtonEntity moduleButtonEntity = buttondata.Find(t => t.F_Id == item.F_ItemId);
                        authorizeurldata.Add(new AuthorizeActionModel { F_Id = moduleButtonEntity.F_ModuleId, F_UrlAddress = moduleButtonEntity.F_UrlAddress });
                    }
                }
                CacheFactory.Cache().WriteCache(authorizeurldata, "authorizeurldata_" + roleId, DateTime.Now.AddMinutes(5));
            }
            else
            {
                authorizeurldata = cachedata;
            }
            authorizeurldata = authorizeurldata.FindAll(t => t.F_Id.Equals(moduleId));
            foreach (var item in authorizeurldata)
            {
                if (!string.IsNullOrEmpty(item.F_UrlAddress))
                {
                    string[] url = item.F_UrlAddress.Split('?');
                    if (item.F_Id == moduleId && url[0] == action)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
