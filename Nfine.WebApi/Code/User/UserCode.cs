using NFine.Application.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nfine.WebApi.Code.User
{
    public class UserCode : IUser
    {
        private UserApp UserApp = new UserApp();

        public NFine.Domain.Entity.SystemManage.UserEntity Login(string UserName, string PassWord)
        {
            return UserApp.CheckLogin(UserName, PassWord);
        }

        public NFine.Domain.Entity.SystemManage.UserEntity GetUserInfo(string UserId)
        {
            return UserApp.GetForm(UserId);
        }
    }
}