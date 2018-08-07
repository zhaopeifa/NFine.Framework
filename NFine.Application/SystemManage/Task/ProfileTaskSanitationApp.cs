using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.SystemManage
{
    /// <summary>
    /// 环评-环卫-任务
    /// </summary>
    public class ProfileTaskSanitationApp
    {
        private ProfileTaskSanitationRepository service = new ProfileTaskSanitationRepository();

        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ProfileTaskSanitationEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pagination">分页，排序参数</param>
        /// <param name="keyword">检索关键字</param>
        /// <returns></returns>
        public List<ProfileTaskSanitationEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ProfileTaskSanitationEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_EnCode.Contains(keyword));
            }

            return service.FindList(expression, pagination);
        }

        /// <summary>
        /// 根据id获取单挑数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ProfileTaskSanitationEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
    }
}
