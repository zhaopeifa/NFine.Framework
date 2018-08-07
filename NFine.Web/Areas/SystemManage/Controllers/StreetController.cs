using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class StreetController : ControllerBase
    {
        private ProfileCityApp CityApp = new ProfileCityApp();
        private ProfileCountyApp CountyApp = new ProfileCountyApp();
        private ProfileStreetApp StreetApp = new ProfileStreetApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeEnableJson()
        {
            var cityData = CityApp.GetList();
            var countyData = CountyApp.GetList();

            var treeList = new List<TreeViewModel>();

            TreeViewModel tree = null;
            //初始化一级 城市节点
            foreach (var item in cityData)
            {
                tree = new TreeViewModel();

                tree.id = item.F_Id;
                tree.text = item.CityName;
                tree.value = "1";
                tree.parentId = "0";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = true;
                treeList.Add(tree);
            }
            //初始化二级菜单 区县节点
            foreach (var item in countyData)
            {
                tree = new TreeViewModel();

                tree.id = item.F_Id;
                tree.text = item.CountyName;
                tree.value = "2";
                tree.parentId = item.CityId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                treeList.Add(tree);
            }

            return Content(treeList.TreeViewJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableGridJson()
        {
            string sql = "SELECT * FROM dbo.ProfileStreet ";
            var data = StreetApp.FildSql(sql);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableGridJsonByCountyId(string CountyId)
        {
            var data = StreetApp.GetDictionary(d => d.CountyId == CountyId);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableGridJsonByCountyId_CompanyId(string CountyId, string CompanyId)
        {
            var data = StreetApp.GetDictionary(d => d.CountyId == CountyId && d.CompanyId == CompanyId);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string itemId, string keyword)
        {
            var data = new
            {
                rows = StreetApp.GetList(itemId, pagination, keyword),
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
            var data = StreetApp.GetForm(keyValue);
            if (data.F_LastModifyUserId != null)
            {
                var data1 = StreetApp.GetForm(data.F_LastModifyUserId);

                if (data1 != null)
                {
                    data.F_LastModifyUserId = data1.StreetName;
                }
            }
            if (data.F_CreatorUserId != null)
            {
                var data2 = StreetApp.GetForm(data.F_CreatorUserId);
                if (data2 != null)
                {
                    data.F_CreatorUserId = data2.StreetName;
                }

            }
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProfileStreetEntity steetEntity, string keyValue)
        {
            StreetApp.SubmitForm(steetEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            StreetApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}