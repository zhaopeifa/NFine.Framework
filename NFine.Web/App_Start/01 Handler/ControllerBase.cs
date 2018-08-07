using NFine.Code;
using System.Web.Mvc;

namespace NFine.Web
{
    [HandlerLogin]
    public abstract class ControllerBase : Controller
    {
        public Log FileLog
        {
            get { return LogFactory.GetLogger(this.GetType().ToString()); }
        }
        [HttpGet]
        [HandlerAuthorize]
        public virtual ActionResult Index(string F_Id)
        {
            ViewBag.F_Id = F_Id;
            ViewBag.UserId = OperatorProvider.Provider.GetCurrent().UserId;
            return View();
        }
        [HttpGet]
        [HandlerAuthorize]
        public virtual ActionResult Form()
        {
            ViewBag.UserId = OperatorProvider.Provider.GetCurrent().UserId;
            return View();
        }
        [HttpGet]
        [HandlerAuthorize]
        public virtual ActionResult Details()
        {
            ViewBag.UserId = OperatorProvider.Provider.GetCurrent().UserId;
            return View();
        }
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }
    }
}
