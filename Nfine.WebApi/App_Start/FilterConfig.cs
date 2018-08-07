using Nfine.WebApi.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Nfine.WebApi.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(HttpConfiguration config)
        {
            config.Filters.Add(new ExceptionAttribute());
        }
    }
}