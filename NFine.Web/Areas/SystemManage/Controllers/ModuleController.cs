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
using NFine.Application.Function;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class ModuleController : ControllerBase
    {

        

        private ModuleApp moduleApp = new ModuleApp();

        private FontAweSomeApp fontAweSomeApp = new FontAweSomeApp();
        private UserApp userApp = new UserApp();
        //获取下拉选json
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = moduleApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (ModuleEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_FullName;
                treeModel.parentId = item.F_ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        //获取按钮列表
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetMenuButtonList(string menu_id)
        {
            var roleId = OperatorProvider.Provider.GetCurrent().RoleId;
            var data = new RoleAuthorizeApp().GetButtonList(roleId, menu_id);
           

            return Content(data.ToJson());
        }



        //获取启用系统菜单
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeEnabledSelectJson()
        {

            var data = moduleApp.GetEnabledList();
            var treeList = new List<TreeSelectModel>();
            foreach (ModuleEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_FullName;
                treeModel.parentId = item.F_ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());

            //var sql = "select * from Sys_Module where F_EnabledMark ='1' order by F_SortCode";

            //var data = moduleApp.FildSql(sql);
            //var treeList = new List<TreeSelectModel>();
            //foreach (ModuleEntity item in data)
            //{
            //    TreeSelectModel treeModel = new TreeSelectModel();
            //    treeModel.id = item.F_Id;
            //    treeModel.text = item.F_FullName;
            //    treeModel.parentId = item.F_ParentId;
            //    treeList.Add(treeModel);
            //}
            //return Content(treeList.TreeSelectJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = moduleApp.GetList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.F_FullName.Contains(keyword));
            }
            var treeList = new List<TreeGridModel>();
            foreach (ModuleEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                treeModel.id = item.F_Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.F_ParentId;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = moduleApp.GetForm(keyValue);
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
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ModuleEntity moduleEntity, string keyValue)
        {
            moduleApp.SubmitForm(moduleEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            moduleApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }


        public virtual ActionResult Form1()
        {
            return View();
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetfontAweSomeJson()
        {
            var data = fontAweSomeApp.GetList();

            return Content(data.ToJson());
        }

    }
}
