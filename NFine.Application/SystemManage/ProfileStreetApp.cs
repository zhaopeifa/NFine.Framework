using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Repository.SystemManage;
using NFine.Web.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.SystemManage
{
    public class ProfileStreetApp
    {
        private ProfileStreetRepository service = new ProfileStreetRepository();

        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ProfileStreetEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取城市列表列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<ProfileStreetEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ProfileStreetEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => !(bool)t.F_DeleteMark);
                expression = expression.And(t => t.StreetName.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }

        public List<ProfileStreetEntity> GetList(string CountyId, Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ProfileStreetEntity>();

            expression = expression.And(t => t.CountyId == CountyId);

            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => !(bool)t.F_DeleteMark);
                expression = expression.And(t => t.StreetName.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }


        public List<ProfileStreetEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<ProfileStreetEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.StreetName.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.F_CreatorTime).ToList();
        }

        public List<KeyValuePair<string, string>> GetList(Func<ProfileStreetEntity, bool> whereFun)
        {
            return service.dbcontext.Set<ProfileStreetEntity>().Where(whereFun).Select(d => new KeyValuePair<string, string>(d.F_Id, d.StreetName)).ToList();
        }

        /// <summary>
        /// 获取key value集合 
        /// key:主键
        /// Value:主路名称
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetDictionary(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);

            return service.dbcontext.Database.SqlQuery<ProfileStreetEntity>(strSql.ToString()).Select(d => new KeyValuePair<string, string>(d.F_Id, d.StreetName)).ToList();
        }

        /// <summary>
        /// 获取key value集合 
        /// key:主键
        /// Value:主路名称
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetDictionary(Func<ProfileStreetEntity, bool> whereFun)
        {
            return service.dbcontext.Set<ProfileStreetEntity>().Where(whereFun).Select(d => new KeyValuePair<string, string>(d.F_Id, d.StreetName)).ToList();
        }

        /// <summary>
        /// 获取key value集合 
        /// key:主键
        /// Value:主路名称
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ProfileStreetEntity> GetList(Func<ProfileStreetEntity, ProfileStreetEntity> selectFun, Func<ProfileStreetEntity, bool> whereFun)
        {
            return service.dbcontext.Set<ProfileStreetEntity>().Where(whereFun).Select(selectFun).ToList();
        }

        /// <summary>
        /// 查询、修改、删除用户信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ProfileStreetEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            var udateModel = GetForm(keyValue);
            udateModel.F_DeleteMark = true;
            service.Update(udateModel);
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除街道信息【" + GetForm(keyValue).StreetName + "】成功！");
            }
            catch { }
        }

        /// <summary>
        /// 修改添加
        /// </summary>
        /// <param name="cityEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(ProfileStreetEntity streetEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                streetEntity.Modify(keyValue);

                service.Update(streetEntity);

                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改街道信息【" + streetEntity.StreetName + "】成功！");
                }
                catch { }
            }
            else
            {
                streetEntity.Create();

                service.Insert(streetEntity);

                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "新建街道信息【" + streetEntity.StreetName + "】成功！");
                }
                catch { }

            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="userEntity"></param>
        public void UpdateForm(ProfileStreetEntity streetEntity)
        {
            streetEntity.Modify(streetEntity.F_Id);

            service.Update(streetEntity);
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改街道信息【" + streetEntity.StreetName + "】成功！");
            }
            catch { }
        }
    }
}
