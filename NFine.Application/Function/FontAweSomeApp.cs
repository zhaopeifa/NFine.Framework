using NFine.Code;
using NFine.Data;
using NFine.Domain.Entity.Function;
/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFineFontAweSome
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using NFine.Web.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFine.Application.Function
{
    public class FontAweSomeApp
    {
        private RepositoryBase<FontAweSomeEntity> service = new RepositoryBase<FontAweSomeEntity>();

        /// <summary>
        /// 获得全部的集合
        /// </summary>
        /// <returns></returns>
        public List<FontAweSomeEntity> GetList()
        {
            return service.IQueryable().ToList();
        }
        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<FontAweSomeEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString()).ToList();
        }
        /// <summary>
        /// 获得集合 table使用
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<FontAweSomeEntity> GetList(Pagination pagination, string keyword = "")
        {
            var expression = ExtLinq.True<FontAweSomeEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                //expression = expression.And(t => t.FormName.Contains(keyword));
                //expression = expression.Or(t => t.D.Contains(keyword));
                //expression = expression.Or(t => t.address.Contains(keyword));
                //expression = expression.Or(t => t.uidName.Contains(keyword));
                //expression = expression.Or(t => t.Number.ToString().Contains(keyword));
            }
            //expression = expression.And(t => t.F_Account != "admin");
            return service.FindList(expression, pagination);
        }
        /// <summary>
        /// 根据id获得model
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public FontAweSomeEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            try
            {
                ////添加日志
                //LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除表单模板【" + GetForm(keyValue).FormName + "】成功！");
            }
            catch { }
            service.Delete(t => t.F_Id == keyValue);
        }
        /// <summary>
        ///修改/添加
        /// </summary>
        /// <param name="HouseEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(FontAweSomeEntity HouseEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //HouseEntity.Modify(keyValue);
                service.Update(HouseEntity);
                try
                {
                    //添加日志
                    //LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改表单模板为【" + HouseEntity.FormName + "】成功！");
                }
                catch { }
            }
            else
            {
                //HouseEntity.Create();
                service.Insert(HouseEntity);
                try
                {
                    //添加日志
                    //LogMess.addLog(DbLogType.Create.ToString(), "新建成功", "新建表单模板【" + HouseEntity.FormName + "】成功！");
                }
                catch { }
            }
        }

    }
}
