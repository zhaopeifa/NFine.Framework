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
    /// 环评-环卫-机扫车
    /// </summary>
    public class SanitationMachineCleanCarController : ControllerBase
    {
        private ProfileSanitationCarApp App = new ProfileSanitationCarApp();
        private UserApp userApp = new UserApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = App.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult FormWorkItem()
        {
            return View();
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProfileSanitationCarEntity entity, string keyValue)
        {
            App.SubmitForm(entity, keyValue);
            return Success("操作成功!");
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetWorkItemGridJson(Pagination pagination, string keyword)
        {
            var sss = new List<ss>();
            for (int i = 0; i < 10; i++)
            {
                sss.Add(new ss());
            }
            var data = new
            {
                rows = sss,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
    }

    public class ss
    {
        public ss()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_EnCode = Guid.NewGuid().ToString();
            this.Time = Guid.NewGuid().ToString();
            this.MainId = Guid.NewGuid().ToString();
            this.Origin = Guid.NewGuid().ToString();
        }
        public string F_Id { get; set; }
        public string F_EnCode { get; set; }
        public string Time { get; set; }
        public string MainId { get; set; }
        public string Origin { get; set; }
    }
}