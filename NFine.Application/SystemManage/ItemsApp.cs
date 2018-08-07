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
using NFine.Code;
using System.Text;
using NFine.Web.Function;
namespace NFine.Application.SystemManage
{
    public class ItemsApp
    {
        private IItemsRepository service = new ItemsRepository();


        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ItemsEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString()).ToList();
        }

        //获取数据字典类别列表
        public List<ItemsEntity> GetList()
        {
            return service.IQueryable().ToList();
        }
        public List<ItemsEntity> GetList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<ItemsEntity>();
           
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        //获取启用的数据字典类别列表
        public List<ItemsEntity> GetEnableList()
        {
            return service.IQueryable(t => t.F_EnabledMark == true).ToList();
        }

        public List<ItemsEntity> GetEnableList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<ItemsEntity>();

            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.Or(t => t.F_EnabledMark == true);
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }



        //获取、删除、修改信息
        public ItemsEntity GetForm(string keyValue)
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
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除数据字典分类信息【" + GetForm(keyValue).F_FullName + "】成功！");
                }
                catch { }

                service.Delete(t => t.F_Id == keyValue);
            }
        }
        public void SubmitForm(ItemsEntity itemsEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                itemsEntity.Modify(keyValue);
                service.Update(itemsEntity);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改数据字典分类信息【" + itemsEntity.F_FullName + "】成功！");
                }
                catch { }
            }
            else
            {
                itemsEntity.Create();
                service.Insert(itemsEntity);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "新建数据字典分类信息【" + itemsEntity.F_FullName + "】成功！");
                }
                catch { }
            }
        }
    }
}
