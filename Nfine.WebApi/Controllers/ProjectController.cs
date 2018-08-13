using Nfine.WebApi.Contracts;
using Nfine.WebApi.Enums;
using NFine.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nfine.WebApi.Controllers
{
    /// <summary>
    /// 获取项目
    /// </summary>
    public class ProjectController : ApiController
    {
        private Code.Project.IProject ProjectCode = new Code.Project.ProjectCode();

        [HttpGet]
        [HttpPost]
        public IHttpActionResult Get()
        {
            var result = ApiBackParameter<List<ApiProjectContracts>>.Get((api) =>
            {
                api.StatusCode = StatusCodeEnum.成功.GetIntValue();
                api.Data = ProjectCode.GetProject();
            });

            return Ok(result);
        }

        [HttpGet]
        [HttpPatch]
        public IHttpActionResult Get(string countyId)
        {
            var result = ApiBackParameter<List<ApiProjectContracts>>.Get((api) =>
            {
                api.StatusCode = StatusCodeEnum.成功.GetIntValue();
                api.Data = ProjectCode.GetProject(countyId);
            });

            return Ok(result);
        }
    }
}
