/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class DutyController : ControllerBase
    {
        private DutyApp dutyApp = new DutyApp();
        private UserApp userApp = new UserApp();
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string keyword)
        {
            var data = dutyApp.GetList(keyword);
            return Content(data.ToJson());
        }
    

        //获取启用的岗位列表
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableGridJson(string keyword)
        {
            var data = dutyApp.GetEnableList(keyword);
            return Content(data.ToJson());
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = dutyApp.GetForm(keyValue);
            if (data.F_LastModifyUserId != null)
            {
                var data1 = userApp.GetForm(data.F_LastModifyUserId);
                
                if (data1 != null)
                {
                    data.F_LastModifyUserId = data1.F_RealName;
                }
            }
            if (data.F_CreatorUserId != null)
            {
                var data2 = userApp.GetForm(data.F_CreatorUserId);
                if (data2!=null)
                {
                    data.F_CreatorUserId = data2.F_RealName;
                }
                
            }
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(RoleEntity roleEntity, string keyValue)
        {
            dutyApp.SubmitForm(roleEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            dutyApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
