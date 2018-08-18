using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 任务  已派发待审核任务单
    /// </summary>
    public class TaskAuditController : ControllerBase
    {
        private ProfileTaskApp taskApp = new ProfileTaskApp();
        private ProfileSanitationWayApp wayApp = new ProfileSanitationWayApp();
        private UserApp userApp = new UserApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = taskApp.GetContractsList(pagination, keyword, ProfileTaskStateEnum.ToAudit.GetIntValue()),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
    }
}