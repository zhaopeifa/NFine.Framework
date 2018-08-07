using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfine.WebApi.Code.User
{
    public interface IUser
    {
        UserEntity Login(string UserName, string PassWord);
        UserEntity GetUserInfo(string UserId);
    }
}
