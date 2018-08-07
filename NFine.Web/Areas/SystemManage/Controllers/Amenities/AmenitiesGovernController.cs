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
    /// 环评-市容-三年治理计划
    /// </summary>
    public class AmenitiesGovernController : ControllerBase
    {
        private ProfileAmenitiesGovernApp App = new ProfileAmenitiesGovernApp();
        private UserApp userApp = new UserApp();
        private ProfileAmenitiesMainWay_GovernApp AGApp = new ProfileAmenitiesMainWay_GovernApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, string cityId, string countyId, string projectId, string streetId)
        {
            var data = new
            {
                rows = App.GetList(pagination, keyword, projectId, streetId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProfileAmenitiesGovernEntity Entity, string keyValue, string mainWayIdsStr)
        {
            string[] mainWayIds = mainWayIdsStr.Split(',');
            App.SubmitForm(Entity, keyValue, mainWayIds);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            App.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = App.GetForm(keyValue);
            if (data.F_LastModifyUserId != null)
            {
                var data1 = userApp.GetForm(data.F_LastModifyUserId);

                if (data1 != null)
                {
                    data.F_LastModifyUserId = data1.F_RealName;
                }
            }
            if (data.F_CreatorUserId != null)
            {
                var data2 = userApp.GetForm(data.F_CreatorUserId);
                if (data2 != null)
                {
                    data.F_CreatorUserId = data2.F_RealName;
                }

            }
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetMainWayIds(string keyValue)
        {
            string sql = "SELECT * FROM ProfileAmenitiesMainWay_Govern WHERE GovernId='" + keyValue + "'";
            string[] ids = AGApp.FildSql(sql).Select(d => d.MainWayId).ToArray();
            return Content(ids.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableGovernSortGridJson()
        {
            List<object> list = new List<object>();

            foreach (int myCode in Enum.GetValues(typeof(NFine.Domain.Enums.ProfileGovernSortEnum)))
            {
                string strName = Enum.GetName(typeof(NFine.Domain.Enums.ProfileGovernSortEnum), myCode);//获取名称

                list.Add(new { id = myCode, text = strName });
            }
            return Content(list.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableEnvironmentTypeGridJson()
        {
            List<object> list = new List<object>();

            foreach (int myCode in Enum.GetValues(typeof(NFine.Domain.Enums.ProfileEnvironmentTypeEnum)))
            {
                string strName = Enum.GetName(typeof(NFine.Domain.Enums.ProfileEnvironmentTypeEnum), myCode);//获取名称

                list.Add(new { id = myCode, text = strName });
            }
            return Content(list.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableGovernTypeGridJson()
        {
            List<object> list = new List<object>();

            foreach (int myCode in Enum.GetValues(typeof(NFine.Domain.Enums.ProfileGovernTypeEnum)))
            {
                string strName = Enum.GetName(typeof(NFine.Domain.Enums.ProfileGovernTypeEnum), myCode);//获取名称

                list.Add(new { id = myCode, text = strName });
            }
            return Content(list.ToJson());
        }
    }
}