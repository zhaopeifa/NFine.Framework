using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NFine.Application;
using NFine.Application.SystemSecurity;
using NFine.Code;
using NFine.Domain.Entity.SystemSecurity;

namespace NFine.Web.Function
{
    public static class LogMess
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">日志类型 </param>
        /// <param name="name">标题</param>
        /// <param name="mess">内容</param>
        public static void addLog(string type,string name,string mess) {
            try{
                OperatorModel userEntity = OperatorProvider.Provider.GetCurrent();
                LogEntity logEntity = new LogEntity();
                logEntity.F_ModuleName = name;
                logEntity.F_Type = type;
                if (userEntity != null)
                {
                    logEntity.F_Account = userEntity.UserCode;
                    logEntity.F_NickName = userEntity.UserName;
                }
                logEntity.F_Result = true;
                logEntity.F_Description = mess;
                new LogApp().WriteDbLog(logEntity);
            }catch{}
        }
    }
}