using NFine.Application.Function;
using NFine.Domain.Entity.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Code;

namespace NFine.Web.Areas.Function.Controllers
{
    public class ERPNFormController : ControllerBase
    {
        //
        // GET: /Function/ERPNForm/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FromDesigner()
        {

            return View();
        }
        private ERPNFormApp ERPNFormApp = new ERPNFormApp();
  

        //获得最近20条的报警信息
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetERPNFormMess()
        {
            string sql = @"SELECT top 20 * FROM ERPNForm WHERE 1=1 order by F_CreatorTime desc";


            List<ERPNFormEntity> list = ERPNFormApp.FildSql(sql);

            return Content(list.ToJson());
        }

        //获得下拉选的集合
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string enCode)
        {
            string sql = @"SELECT * FROM ERPNForm";
            if (!string.IsNullOrEmpty(enCode))
            {
                sql += " WHERE F_Id ='" + enCode + "'";
            }
            var data = ERPNFormApp.FildSql(sql);
            List<object> list = new List<object>();
            foreach (ERPNFormEntity item in data)
            {
                list.Add(new { id = item.F_Id, text = item.FormName });
            }
            return Content(list.ToJson());
        }
        //获得table集合
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {

            var data = new
            {
                rows = ERPNFormApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());

        }
        //根据id获得model
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = ERPNFormApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ERPNFormEntity organizeEntity, string keyValue)
        {
          
            ERPNFormApp.SubmitForm(organizeEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            ERPNFormApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

    }
}
