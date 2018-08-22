using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Contracts
{
    /// <summary>
    /// 扣分添加接受model
    /// </summary>
    public class ProfileDeducInsSubMitContracts
    {

        public string F_Id { get; set; }

        public string DeducIns_Id { get; set; }

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
        

        public string TaskEntryId { get; set; }

        public string NormId { get; set; }

        public int DeductionSeveral { get; set; }

        public int DeductionScore { get; set; }

        public string DeductionDescribe { get; set; }
    }
}
