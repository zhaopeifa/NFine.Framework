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
    public class CountyController : ControllerBase
    {
        private ProfileCountyApp CountyApp = new ProfileCountyApp();

        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = CountyApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProfileCountyEntity CountyEntity, string keyValue)
        {
            CountyApp.SubmitForm(CountyEntity, keyValue);
            return Success("操作成功。");
        }

        public ActionResult GetFormJson(string keyValue)
        {
            var data = CountyApp.GetForm(keyValue);
            if (data.F_LastModifyUserId != null)
            {
                var data1 = CountyApp.GetForm(data.F_LastModifyUserId);

                if (data1 != null)
                {
                    data.F_LastModifyUserId = data1.CountyName;
                }
            }
            if (data.F_CreatorUserId != null)
            {
                var data2 = CountyApp.GetForm(data.F_CreatorUserId);
                if (data2 != null)
                {
                    data.F_CreatorUserId = data2.CountyName;
                }

            }
            return Content(data.ToJson());
        }

        [HttpGet]
        public ActionResult GetEnableGridJson(string keyword)
        {
            var data = CountyApp.GetList(keyword);
            return Content(data.ToJson());

        }
    }
}