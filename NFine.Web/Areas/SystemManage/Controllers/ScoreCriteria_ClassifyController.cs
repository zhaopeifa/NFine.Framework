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
    public class ScoreCriteria_ClassifyController : ControllerBase
    {
        private ProfileScoreCriteriaApp App = new ProfileScoreCriteriaApp();
        private UserApp userApp = new UserApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = App.GetClassifyDataTable1(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };

            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProfileScoreCriteria_ClassifyEntity Entity, string groupId, string typeIdsStr)
        {
            string[] typeIds = typeIdsStr.Split(',');

            App.SubmitClassifyForm(Entity, groupId, typeIds);
            return Success("操作成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string groupId)
        {
            var data = App.GetClassifyFrom(groupId);

            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTypeAssociated(string groupId)
        {
            var STypeIds = App.GetClassifyAssociatedType(groupId);
            var SEntryId = string.Empty;

            if (STypeIds.Count > 0)
            {
                SEntryId = App.GetTypeForm(STypeIds[0]).SEntryId;
            }

            var data = new
            {

                STypeIds = STypeIds,
                SEntryId = SEntryId
            };

            return Content(data.ToJson());
        }
    }
}