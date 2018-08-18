using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 评分标准-规范
    /// </summary>
    public class ProfileScireCriteria_NormEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string SNormId { get; set; }
        
        /// <summary>
        /// 检查项目
        /// </summary>
        public string SNormProjectName { get; set; }

        /// <summary>
        /// 评分标准
        /// </summary>
        public string SNormStandardName { get; set; }

        /// <summary>
        /// 起扣条件 几处
        /// </summary>
        public int Condition { get; set; }

        /// <summary>
        /// 关联小类Id
        /// </summary>
        public string SClassifyId { get; set; }

        /// <summary>
        /// 分组Id
        /// </summary>
        public string GroupId { get; set; }
    }
}
