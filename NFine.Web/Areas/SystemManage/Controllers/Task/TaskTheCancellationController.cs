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
    /// 已作废任务单
    /// </summary>
    public class TaskTheCancellationController : ControllerBase
    {
        private ProfileTaskApp taskApp = new ProfileTaskApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = taskApp.GetContractsList(pagination, keyword, ProfileTaskStateEnum.TheCancellation.GetIntValue()),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 任务单作废
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult TaskInvalid(string keyValue)
        {
            taskApp.TaskInvalid(keyValue);

            return Success("操作成功。");
        }
    }
}