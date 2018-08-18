using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class ScoreCriteria_TypeController : ControllerBase
    {
        private ProfileScoreCriteriaApp App = new ProfileScoreCriteriaApp();
        private UserApp userApp = new UserApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = App.GetTypeList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };

            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProfileScoreCriteria_TypeEntity Entity, string keyValue)
        {
            App.SubmitTypeForm(Entity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            App.DeleteTypeForm(keyValue);
            return Success("删除成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = App.GetTypeForm(keyValue);

            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取下拉框数据 
        /// </summary>
        /// <param name="entryId">大类筛选</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableGridJson(string entryId)
        {
            StringBuilder sqlStr = new StringBuilder();

            sqlStr.Append("SELECT * FROM ProfileScoreCriteria_Type where 1=1 ");
            if (!string.IsNullOrEmpty(entryId))
            {
                sqlStr.Append(" and SEntryId='" + entryId + "'");
            }

            var data =App.GetTypeDictionary(sqlStr.ToString());

            return Content(data.ToJson());
        }
    }
}