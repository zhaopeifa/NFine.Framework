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
    public class UserController : ApiController
    {
        private Code.User.IUser UserCode = new Code.User.UserCode();

        [HttpGet]
        [HttpPost]
        public IHttpActionResult Login(string UserName, string PassWord)
        {
            UserEntity loginUser = null;
            bool isHaveException = false;
            string errorMess = "";
            try
            {
                loginUser = UserCode.Login(UserName, PassWord);
            }
            catch (Exception ex)
            {
                isHaveException = true;
                errorMess = ex.ToString();
            }

            var result = ApiBackParameter<string>.Get((api) =>
            {
                if (loginUser == null || isHaveException)
                {
                    api.StatusCode = StatusCodeEnum.失败.GetIntValue();
                    api.Message = errorMess;
                }
                else
                {
                    api.StatusCode = StatusCodeEnum.成功.GetIntValue();
                    api.Data = loginUser.F_Id;
                    api.Message = "登录成功!";
                }
            });

            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult GetUserInfo(string UserId)
        {
            var userEntity = UserCode.GetUserInfo(UserId);
            var result = ApiBackParameter<UserEntity>.Get((api) =>
            {
                if (userEntity == null)
                {
                    api.StatusCode = StatusCodeEnum.失败.GetIntValue();
                    api.Message = "查找失败！";
                }
                else
                {
                    api.StatusCode = StatusCodeEnum.成功.GetIntValue();
                    api.Data = userEntity;
                    api.Message = "查找成功！";
                }
            });

            return Ok(result);
        }
    }
}
