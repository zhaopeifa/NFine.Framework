/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
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
    public class OrganizeApp
    {
        private IOrganizeRepository service = new OrganizeRepository();
        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<OrganizeEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString()).ToList();
        }
        //获取组织机构列表
        public List<OrganizeEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.F_CreatorTime).ToList();
        }
        //获取启用的组织机构列表
        public List<OrganizeEntity> GetEnableList()
        {
            return service.IQueryable(t => t.F_EnabledMark == true).OrderBy(t => t.F_CreatorTime).ToList();
        }

        //获取、删除、修改信息
        public OrganizeEntity GetForm(string keyValue)
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
                    LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除组织机构信息【" + GetForm(keyValue).F_FullName + "】成功！");
                }
                catch { }
            }
        }
        public void SubmitForm(OrganizeEntity organizeEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                organizeEntity.Modify(keyValue);
                service.Update(organizeEntity);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改组织机构信息【" + organizeEntity.F_FullName + "】成功！");
                }
                catch { }
            }
            else
            {
                organizeEntity.Create();
                service.Insert(organizeEntity);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "新建组织机构信息【" + organizeEntity.F_FullName + "】成功！");
                }
                catch { }
            }
        }
    }
}
