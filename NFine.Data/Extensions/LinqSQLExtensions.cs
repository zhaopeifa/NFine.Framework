using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Data.Extensions
{
    public class LinqSQLExtensions
    {
        private static NFineDbContext dbcontext = new NFineDbContext();

        /// <summary>
        /// 获取数据库数据上下文
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IQueryable<TEntity> IQueryable<TEntity>() where TEntity : class
        {
            return dbcontext.Set<TEntity>();
        }
    }
}
