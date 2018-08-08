using NFine.Application.SystemManage;
using NFine.Code;
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
        private ProfileTaskApp taskApp = new ProfileTaskApp();
        private ProfileSanitationWayApp wayApp = new ProfileSanitationWayApp();
        private UserApp userApp = new UserApp();

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(TaskInsertContracts Entity, string keyValue)
        {
            taskApp.SubmitForm(Entity, keyValue);
            return Success("操作成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetWayEnableGridJsonByMainWay(string mainWayId)
        {

            var list = wayApp.GetDictionaryToIDMoreThan("SELECT * FROM ProfileSanitationWay WHERE MainWayId='" + mainWayId + "'");

            return Content(list.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetUserEnableGridByOutworker()
        {
            var list = userApp.FildSql("SELECT * FROM Sys_User WHERE F_IsOutworker=1").Select(d => new KeyValuePair<string, string>(d.F_Id, d.F_RealName)).ToList();

            return Content(list.ToJson());
        }
    }
}