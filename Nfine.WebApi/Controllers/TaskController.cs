using Nfine.WebApi.Contracts;
using Nfine.WebApi.Data.Enums;
using Nfine.WebApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nfine.WebApi.Controllers
{
    public class TaskController : ApiController
    {
        private Code.Task.ITask code = new Code.Task.TaskCode();


        [HttpGet]
        public IHttpActionResult Get(string userId, int checkPointCode)
        {

            var type = (CheckPointClassificationEnum)checkPointCode;
            bool isHaveErrpr = false;
            string errorMessage = string.Empty;

            Contracts.ApiWayContracts[] data = null;

            switch (type)
            {
                case CheckPointClassificationEnum.特级道路:
                case CheckPointClassificationEnum.一级道路:
                case CheckPointClassificationEnum.二级道路:
                case CheckPointClassificationEnum.三级道路:
                case CheckPointClassificationEnum.背街小巷:
                    try
                    {
                        data = code.GetWayTask(userId, type);
                    }
                    catch (Exception ex)
                    {
                        isHaveErrpr = true;
                        errorMessage = ex.ToString();
                    }
                    break;
                case CheckPointClassificationEnum.沿街箱房:
                    break;
                case CheckPointClassificationEnum.非沿街箱房:
                    break;
                case CheckPointClassificationEnum.沿街压缩站:
                    break;
                case CheckPointClassificationEnum.非沿街压缩站:
                    break;
                case CheckPointClassificationEnum.一类公厕:
                    break;
                case CheckPointClassificationEnum.二类公厕:
                    break;
                case CheckPointClassificationEnum.三类公厕:
                    break;
                case CheckPointClassificationEnum.倒粪站小便池:
                    break;
                case CheckPointClassificationEnum.机扫车:
                    break;
                case CheckPointClassificationEnum.冲洗车:
                    break;
                case CheckPointClassificationEnum.清运车:
                    break;
                case CheckPointClassificationEnum.电动机具:
                    break;
                case CheckPointClassificationEnum.绿化带:
                    break;
                case CheckPointClassificationEnum.绿色账户小区:
                    break;
                default:
                    break;
            }

            var result = ApiBackParameter<Contracts.ApiWayContracts[]>.Get((api) =>
            {
                if (isHaveErrpr)
                {
                    api.StatusCode = StatusCodeEnum.失败.GetIntValue();
                    api.Message = errorMessage;
                }
                else
                {
                    api.StatusCode = StatusCodeEnum.成功.GetIntValue();
                    api.Data = data;
                }
            });

            return Ok(result);
        }
    }
}
