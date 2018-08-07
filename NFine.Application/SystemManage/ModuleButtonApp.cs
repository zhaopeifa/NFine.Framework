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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFine.Web.Function;

namespace NFine.Application.SystemManage
{
    public class ModuleButtonApp
    {
        private IModuleButtonRepository service = new ModuleButtonRepository();
        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ModuleButtonEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString()).ToList();
        }
        //获取按钮列表
        public List<ModuleButtonEntity> GetList(string moduleId = "")
        {
            var expression = ExtLinq.True<ModuleButtonEntity>();
            if (!string.IsNullOrEmpty(moduleId))
            {
                expression = expression.And(t => t.F_ModuleId == moduleId);
            }
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }


        //获取、删除、修改信息
        public ModuleButtonEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.F_Id == keyValue);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除菜单按钮信息【" + GetForm(keyValue).F_FullName + "】成功！");
                }
                catch { }
            }
        }
        public void SubmitForm(ModuleButtonEntity moduleButtonEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                moduleButtonEntity.Modify(keyValue);
                service.Update(moduleButtonEntity);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改菜单按钮信息【" + moduleButtonEntity.F_FullName + "】成功！");
                }
                catch { }
            }
            else
            {
                moduleButtonEntity.Create();
                service.Insert(moduleButtonEntity);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "新建菜单按钮信息【" + moduleButtonEntity.F_FullName + "】成功！");
                }
                catch { }
            }
        }
        public void SubmitCloneButton(string moduleId, string Ids)
        {
            string[] ArrayId = Ids.Split(',');
            var data = this.GetList();
            var names = "";
            List<ModuleButtonEntity> entitys = new List<ModuleButtonEntity>();
            foreach (string item in ArrayId)
            {
                ModuleButtonEntity moduleButtonEntity = data.Find(t => t.F_Id == item);
                moduleButtonEntity.F_Id = Common.GuId();
                moduleButtonEntity.F_ModuleId = moduleId;
                names += moduleButtonEntity.F_FullName + ",";
                entitys.Add(moduleButtonEntity);
            }
            service.SubmitCloneButton(entitys);
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "克隆按钮信息【" + names + "】成功！");
            }
            catch { }
        }
    }
}
