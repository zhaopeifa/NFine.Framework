using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Code.Web;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class SanitationWayController : ControllerBase
    {
        private ProfileSanitationWayApp wayApp = new ProfileSanitationWayApp();
        private UserApp userApp = new UserApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, string cityId, string countyId, string projectId, string streetId)
        {
            var data = new
            {
                rows = wayApp.GetList(pagination, keyword, projectId, streetId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProfileSanitationWayEntity WayEntity, string keyValue)
        {
            wayApp.SubmitForm(WayEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            wayApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 获取道路等级
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableWayGradeGridJson()
        {

            List<object> list = new List<object>();

            foreach (int myCode in Enum.GetValues(typeof(NFine.Domain.Enums.ProfileWayGradeEnum)))
            {
                string strName = Enum.GetName(typeof(NFine.Domain.Enums.ProfileWayGradeEnum), myCode);//获取名称

                list.Add(new { id = myCode, text = strName });
            }
            return Content(list.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = wayApp.GetForm(keyValue);
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


    }
}