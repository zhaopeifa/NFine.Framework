using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Contracts
{
    public class ScorCriteriaClassifyTreeGridContracts
    {
        /// <summary>
        /// 类型  1:小类  2:评分明细
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// treerGridb父级菜单
        /// </summary>
        public string F_ParentId { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string F_Id { get; set; }

        public string SEntryName { get; set; }

        public string STypeName { get; set; }

        /// <summary>
        /// 小类名
        /// </summary>
        public string SClassifyName { get; set; }

        /// <summary>
        /// 总分值
        /// </summary>
        public int SClassifyScore { get; set; }


        /// <summary>
        /// 评分明细项目名
        /// </summary>
        public string SNormProjectName { get; set; }

        /// <summary>
        /// 评分明细 评分标准
        /// </summary>
        public string SNormStandardName { get; set; }

        /// <summary>
        /// 起扣条件
        /// </summary>
        public int? SNormCondition { get; set; }
    }
}
