using NFine.Application.SystemSecurity;
/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.Entity.SystemSecurity;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Text;
using NFine.Web.Function;
namespace NFine.Application.SystemManage
{
    public class UserApp
    {
        private UserRepository service = new UserRepository();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();
        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<UserEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString());
        }

        //获取用户列表
        public List<UserEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<UserEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Account.Contains(keyword));
                expression = expression.Or(t => t.F_RealName.Contains(keyword));
                expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
            }
            expression = expression.And(t => t.F_Account != "admin");
            return service.FindList(expression, pagination);
        }

        //查询、修改、删除用户信息
        public UserEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除用户信息【" + GetForm(keyValue).F_RealName + "】成功！");
            }
            catch { }
        }
        public void SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                userEntity.Modify(keyValue);
            
            }
            else
            {
                userEntity.Create();
             
            }
            service.SubmitForm(userEntity, userLogOnEntity, keyValue);

            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改用户信息【" + userEntity.F_RealName + "】成功！");
            }
            catch { }
        }
        public void UpdateForm(UserEntity userEntity)
        {
            service.Update(userEntity);
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改用户信息【" + userEntity.F_RealName + "】成功！");
            }
            catch { }
        }


        //登录
        public UserEntity CheckLogin(string username, string password)
        {
            bool DL = true;
            string ip = Net.Ip;
            //获取访问通过的ip
            List<FilterIPEntity> list = new FilterIPApp().GetList();
            if (list != null && list.Count != 0)
            {
                foreach (FilterIPEntity fi in list)
                {
                    bool b = Net.isinip(ip, fi.F_StartIP, fi.F_EndIP);
                    if (b)
                    {
                        DL = false;
                        break;

                    }
                }

                if (DL)
                {
                    UserEntity userEntity = service.FindEntity(t => t.F_Account == username);
                    if (userEntity != null)
                    {
                        if (userEntity.F_EnabledMark == true)
                        {
                            UserLogOnEntity userLogOnEntity = userLogOnApp.GetForm(userEntity.F_Id);
                            string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                            if (dbPassword == userLogOnEntity.F_UserPassword)
                            {
                                DateTime lastVisitTime = DateTime.Now;
                                int LogOnCount = (userLogOnEntity.F_LogOnCount).ToInt() + 1;
                                if (userLogOnEntity.F_LastVisitTime != null)
                                {
                                    userLogOnEntity.F_PreviousVisitTime = userLogOnEntity.F_LastVisitTime.ToDate();
                                }
                                userLogOnEntity.F_LastVisitTime = lastVisitTime;
                                userLogOnEntity.F_LogOnCount = LogOnCount;
                                userLogOnApp.UpdateForm(userLogOnEntity);
                                return userEntity;
                            }
                            else
                            {
                                throw new Exception("密码不正确，请重新输入");
                            }
                        }
                        else
                        {
                            throw new Exception("账户被系统锁定,请联系管理员");
                        }
                    }
                    else
                    {
                        throw new Exception("账户不存在，请重新输入");
                    }
                }
                else {
                    throw new Exception("您的ip被禁用了！");
                }
            }
            else {
                UserEntity userEntity = service.FindEntity(t => t.F_Account == username);
                if (userEntity != null)
                {
                    if (userEntity.F_EnabledMark == true)
                    {
                        UserLogOnEntity userLogOnEntity = userLogOnApp.GetForm(userEntity.F_Id);
                        string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                        if (dbPassword == userLogOnEntity.F_UserPassword)
                        {
                            DateTime lastVisitTime = DateTime.Now;
                            int LogOnCount = (userLogOnEntity.F_LogOnCount).ToInt() + 1;
                            if (userLogOnEntity.F_LastVisitTime != null)
                            {
                                userLogOnEntity.F_PreviousVisitTime = userLogOnEntity.F_LastVisitTime.ToDate();
                            }
                            userLogOnEntity.F_LastVisitTime = lastVisitTime;
                            userLogOnEntity.F_LogOnCount = LogOnCount;
                            userLogOnApp.UpdateForm(userLogOnEntity);
                            return userEntity;
                        }
                        else
                        {
                            throw new Exception("密码不正确，请重新输入");
                        }
                    }
                    else
                    {
                        throw new Exception("账户被系统锁定,请联系管理员");
                    }
                }
                else
                {
                    throw new Exception("账户不存在，请重新输入");
                }
            }

        }
    }
}
