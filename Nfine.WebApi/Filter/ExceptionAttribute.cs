using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Nfine.WebApi.Filter
{
    public class ExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var oResponse = new HttpResponseMessage();
            if (actionExecutedContext.Exception is NotImplementedException)
            {
                oResponse = new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message)
                };
            }
            else if (actionExecutedContext.Exception is UnauthorizedAccessException)
            {
                oResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message)
                };
            }
            else if (actionExecutedContext.Exception is TimeoutException)
            {
                oResponse = new HttpResponseMessage(HttpStatusCode.RequestTimeout)
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message)
                };
            }
            else
            {
                oResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message)
                };
            }
            actionExecutedContext.Response = oResponse;
            base.OnException(actionExecutedContext);


        }

        private void WriteLog(HttpActionExecutedContext context)
        {
            if (context == null)
                return;
            var log = LogFactory.GetLogger(context.ActionContext.ToString());
            log.Error(context.Exception);
        }
    }
}