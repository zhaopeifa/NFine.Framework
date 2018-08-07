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
    public class ItemsDetailApp
    {
        private IItemsDetailRepository service = new ItemsDetailRepository();
        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ItemsDetailEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString()).ToList();
        }
        //获取数据字典列表
        public List<ItemsDetailEntity> GetList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<ItemsDetailEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_ItemId == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_ItemName.Contains(keyword));
                expression = expression.Or(t => t.F_ItemCode.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }
      

        //获取启用的数据字典列表

        public List<ItemsDetailEntity> GetEnableList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<ItemsDetailEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_ItemId == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_ItemName.Contains(keyword));
                expression = expression.Or(t => t.F_ItemCode.Contains(keyword));
            }
            expression = expression.Or(t => t.F_EnabledMark == true);
            return service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        public List<ItemsDetailEntity> GetItemList(string enCode)
        {
            return service.GetItemList(enCode);
        }


        //获取、删除、修改信息
        public ItemsDetailEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除数据字典信息【" + GetForm(keyValue).F_ItemName + "】成功！");
            }
            catch { }
            service.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(ItemsDetailEntity itemsDetailEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                itemsDetailEntity.Modify(keyValue);
                service.Update(itemsDetailEntity);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改数据字典信息【" + itemsDetailEntity.F_ItemName + "】成功！");
                }
                catch { }
            }
            else
            {
                itemsDetailEntity.Create();
                service.Insert(itemsDetailEntity);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "新建数据字典信息【" + itemsDetailEntity.F_ItemName + "】成功！");
                }
                catch { }
            }
        }
    }
}
