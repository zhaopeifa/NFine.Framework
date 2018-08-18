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
    public class TaskHavePutAnEndToController : ControllerBase
    {
        private ProfileTaskApp taskApp = new ProfileTaskApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = taskApp.GetContractsList(pagination, keyword, ProfileTaskStateEnum.HavePutAnEndTo.GetIntValue()),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
    }
}