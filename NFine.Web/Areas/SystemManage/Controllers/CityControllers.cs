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
    public class CityController : ControllerBase
    {
        private ProfileCityApp CityApp = new ProfileCityApp();
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = CityApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        public ActionResult GetEnableGridJson(string keyword)
        {
            var data = CityApp.GetList(keyword);
            return Content(data.ToJson());

        }
        
    }
}