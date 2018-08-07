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
    public class ItemsTypeController : ControllerBase
    {
        private ItemsApp itemsApp = new ItemsApp();
        private UserApp userApp = new UserApp();
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = itemsApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (ItemsEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_FullName;
                treeModel.parentId = item.F_ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson()
        {
            var data = itemsApp.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (ItemsEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_Id) == 0 ? false : true;
                tree.id = item.F_Id;
                tree.text = item.F_FullName;
                tree.value = item.F_EnCode;
                tree.parentId = item.F_ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }

        //获取启用的数据字典内容
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectEnableJson()
        {
            var data = itemsApp.GetEnableList();
            var treeList = new List<TreeSelectModel>();
            foreach (ItemsEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.F_Id;
                treeModel.text = item.F_FullName;
                treeModel.parentId = item.F_ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        //获取启用的数据字典内容
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeEnableJson()
        {
            var data = itemsApp.GetEnableList();
            var treeList = new List<TreeViewModel>();
            foreach (ItemsEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_Id && t.F_EnabledMark == true) == 0 ? false : true;
                tree.id = item.F_Id;
                tree.text = item.F_FullName;
                tree.value = item.F_EnCode;
                tree.parentId = item.F_ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string itemId, string keyword)
        {
            var data = itemsApp.GetList(itemId, keyword);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson()
        {
            var data = itemsApp.GetList();
            var treeList = new List<TreeGridModel>();
            foreach (ItemsEntity item in data)
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
            var data = itemsApp.GetForm(keyValue);
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
        public ActionResult SubmitForm(ItemsEntity itemsEntity, string keyValue)
        {
            itemsApp.SubmitForm(itemsEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            itemsApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
