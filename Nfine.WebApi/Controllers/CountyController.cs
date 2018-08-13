using Nfine.WebApi.Contracts;
using Nfine.WebApi.Enums;
using NFine.Domain.Contracts;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nfine.WebApi.Controllers
{
    /// <summary>
    /// 环评区县信息
    /// </summary>
    public class CountyController : ApiController
    {
        Code.County.ICounty CountyCode = new Code.County.CountyCode();


        /// <summary>
        /// 获取区县  暂时只获取上海下的区县
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        public IHttpActionResult Get()
        {
            var result = ApiBackParameter<List<ApiProfileCountyContracts>>.Get((api) =>
            {
                api.StatusCode = StatusCodeEnum.成功.GetIntValue();
                api.Data = CountyCode.GetProfileCountyEntitys(null);
            });

            return Ok(result);
        }

        [HttpGet]
        [HttpPost]
        public IHttpActionResult Get(string cityId)
        {
            var result = ApiBackParameter<List<ApiProfileCountyContracts>>.Get((api) =>
            {
                api.StatusCode = StatusCodeEnum.成功.GetIntValue();
                api.Data = CountyCode.GetProfileCountyEntitys(cityId);
            });

            return Ok(result);
        }
    }
}
