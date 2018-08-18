using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 评分标准小类表 
    /// </summary>
    public class ProfileScoreCriteria_ClassifyEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string SClassifyId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string SClassifyName { get; set; }

        /// <summary>
        /// 分值
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 关联中类名称
        /// </summary>
        public string STypeId { get; set; }

        public string GroupId { get; set; }
    }
}
