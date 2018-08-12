using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Contracts;
using NFine.Domain.Enums;
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


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = taskApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(TaskInsertContracts Entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (taskApp.GetForm(keyValue).State != ProfileTaskStateEnum.NotToSend.GetIntValue())
                {
                    throw new Exception("当前任务单禁止修改，只有未派发任务才允许修改!");
                }
            }

            taskApp.SubmitForm(Entity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            var deleteModel = taskApp.GetForm(keyValue);
            if (deleteModel.State != ProfileTaskStateEnum.NotToSend.GetIntValue())
            {
                throw new Exception("任务但只有未派遣状态下才允许删除操作!");
            }

            taskApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = taskApp.GetTaskInsertContractsForm(keyValue);


            return Content(data.ToJson());
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

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult TaskSend(string keyValue)
        {
            taskApp.TaskDistributed(keyValue);

            return Success("操作成功。");
        }

        /// <summary>
        /// 获取检查点数量
        /// </summary>
        /// <param name="streetId">根据街道筛选</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetCheckPointCount(string streetId)
        {
            var countContracts = taskApp.GetCheckPostCount(streetId);

            return Content(countContracts.ToJson());
        }
    }
}