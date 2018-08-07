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
    public class UserController : ControllerBase
    {
        private UserApp userApp = new UserApp();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();


        private ItemsDetailApp itemsDetailApp = new ItemsDetailApp();

        private OrganizeApp organizeApp = new OrganizeApp();

        private RoleApp roleApp = new RoleApp();

        private DutyApp dutyApp = new DutyApp();

      
        //获得最近20条的报警信息
      
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            string sql = @"SELECT  * FROM Sys_User WHERE 1=1 and F_Account <> 'admin' order by F_CreatorTime desc";
            List<UserEntity> list = userApp.FildSql(sql);

            return Content(list.ToJson());
        }
            
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableTreeSelectJson()
        {
            string sql = @"SELECT  * FROM Sys_User WHERE 1=1 and F_Account <> 'admin' and F_EnabledMark = '1' order by F_CreatorTime desc";
            List<UserEntity> list = userApp.FildSql(sql);

            return Content(list.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = userApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = userApp.GetForm(keyValue);
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
                if (data2 != null)
                {
                    data.F_CreatorUserId = data2.F_RealName;
                }

            }
            return Content(data.ToJson());
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJsonDetail(string keyValue)
        {
            var data = userApp.GetForm(keyValue);
            if (data.F_Gender != null)
            {
                string sql = "select * from Sys_ItemsDetail,Sys_Items where Sys_Items.F_Id = Sys_ItemsDetail.F_ItemId and Sys_Items.F_EnCode ='sex' and Sys_ItemsDetail.F_ItemCode = '" + data.F_Gender + "'";
                List<ItemsDetailEntity> ItemsDetailEntityList = itemsDetailApp.FildSql(sql);
                if (ItemsDetailEntityList != null && ItemsDetailEntityList.Count >0)
                {
                    data.F_Gender = ItemsDetailEntityList[0].F_ItemName;
                }
            }

            if (data.F_IsAdministrator != null)
            {
                string sql = "select * from Sys_ItemsDetail,Sys_Items where Sys_Items.F_Id = Sys_ItemsDetail.F_ItemId and Sys_Items.F_EnCode ='userType' and Sys_ItemsDetail.F_ItemCode = '" + data.F_IsAdministrator + "'";
                List<ItemsDetailEntity> ItemsDetailEntityList = itemsDetailApp.FildSql(sql);
                if (ItemsDetailEntityList != null && ItemsDetailEntityList.Count > 0)
                {
                    data.F_IsAdministrator = ItemsDetailEntityList[0].F_ItemName;
                }
            }

            if (data.F_OrganizeId != null)
            {
                var data1 = organizeApp.GetForm(data.F_OrganizeId);

                if (data1 != null)
                {
                    data.F_OrganizeId = data1.F_FullName;
                }

            }

            if (data.F_RoleId != null)
            {
                var data2 = roleApp.GetForm(data.F_RoleId);
                if (data2 != null)
                {
                    data.F_RoleId = data2.F_FullName;
                }

            }
           
            if (data.F_DutyId != null)
            {
                var data3 = roleApp.GetForm(data.F_DutyId);
                if (data3 != null)
                {
                    data.F_DutyId = data3.F_FullName;
                }
            
            }
           
            //if (data.F_LastModifyUserId != null)
            //{
            //    var data1 = userApp.GetForm(data.F_LastModifyUserId);
            //    data.F_LastModifyUserId = data1.F_RealName;
            //}
            //if (data.F_CreatorUserId != null)
            //{
            //    var data2 = userApp.GetForm(data.F_CreatorUserId);
            //    data.F_CreatorUserId = data2.F_RealName;
            //}
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            userApp.SubmitForm(userEntity, userLogOnEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            userApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        [HttpGet]
        public ActionResult RevisePassword()
        {
            return View();
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitRevisePassword(string userPassword, string keyValue)
        {
            userLogOnApp.RevisePassword(userPassword, keyValue);
            return Success("重置密码成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledAccount(string keyValue)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.F_Id = keyValue;
            userEntity.F_EnabledMark = false;
            userApp.UpdateForm(userEntity);
            return Success("账户禁用成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledAccount(string keyValue)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.F_Id = keyValue;
            userEntity.F_EnabledMark = true;
            userApp.UpdateForm(userEntity);
            return Success("账户启用成功。");
        }

        [HttpGet]
        public ActionResult Info()
        {
            ViewBag.UserId = OperatorProvider.Provider.GetCurrent().UserId;
            return View();
        }
    }
}
