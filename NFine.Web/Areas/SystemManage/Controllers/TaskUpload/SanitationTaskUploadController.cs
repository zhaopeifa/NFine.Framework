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
    /// 环卫-任务上传
    /// </summary>
    public class SanitationTaskUploadController : ControllerBase
    {

        private ProfileTaskApp taskApp = new ProfileTaskApp();
        /// <summary>
        /// 评分标准
        /// </summary>
        private ProfileScoreCriteriaApp scApp = new ProfileScoreCriteriaApp();


        [HttpGet]
        public ActionResult TaskDetail()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = taskApp.GetNeedUpLoadTask(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        public ActionResult UpLoadTaskEntrFormy()
        {
            return View();
        }

        /// <summary>
        /// 加载评分标准
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetScireCriteria(string taskEntryId)
        {




            return null;
        }
    }
}