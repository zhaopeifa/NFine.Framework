using NFine.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 环卫-任务派发
    /// </summary>
    public class TaskDistributingController : ControllerBase
    {
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(TaskInsertContracts Entity, string keyValue)
        {
            return null;
        }
    }
}