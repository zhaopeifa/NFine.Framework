using NFine.Data;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.SystemManage
{
    /// <summary>
    /// 环评-环卫-公厕
    /// </summary>
    public class ProfileSanitationTandasRepository : RepositoryBase<ProfileSanitationTandasEntity>
    {
        /// <summary>
        /// 批量添加
        /// </summary>
        public int SubmitDatas(List<ProfileSanitationTandasEntity> tandasEntitys)
        {
            int affectedRows = 0;
            using (var db = new RepositoryBase().BeginTrans())
            {
                affectedRows = db.Insert(tandasEntitys);
            }
            return affectedRows;
        }

        /// <summary>
        /// 操作 存在事物
        /// </summary>
        /// <param name="callBack">回调函数 返回操作数据库对象</param>
        public void Command(Action<DbTransaction> callBack)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Command(callBack);
            }
        }
    }
}
