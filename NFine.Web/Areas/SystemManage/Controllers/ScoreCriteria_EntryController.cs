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
    /// <summary>
    /// 评分标准大类
    /// </summary>
    public class ScoreCriteria_EntryController : ControllerBase
    {
        private ProfileScoreCriteriaApp App = new ProfileScoreCriteriaApp();
        private UserApp userApp = new UserApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = App.GetEntryList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };

            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProfileScoreCriteria_EntryEntity Entity, string keyValue)
        {
            App.SubmitEntryForm(Entity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            App.DeleteEntryForm(keyValue);
            return Success("删除成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = App.GetEntryForm(keyValue);

            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableGridJson()
        {
            var data = App.GetEntryDictionary("SELECT * FROM ProfileScoreCriteria_Entry");

            return Content(data.ToJson());
        }
    }
}