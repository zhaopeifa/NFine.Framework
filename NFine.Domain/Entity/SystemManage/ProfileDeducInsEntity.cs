using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.SystemManage
{
    /// <summary>
    /// 扣分记录表
    /// </summary>
    public class ProfileDeducInsEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string DeducIns_Id { get; set; }

        /// <summary>
        /// 关联任务明细表
        /// </summary>
        public string TaskEntry_Id { get; set; }

        /// <summary>
        /// 关联评分标准扣分明细表
        /// </summary>
        public string SCNorm_Id { get; set; }

        /// <summary>
        /// 评分标准大类
        /// </summary>
        public string SCEntryName { get; set; }

        /// <summary>
        /// 评分标准中类
        /// </summary>
        public string SCTypeName { get; set; }

        /// <summary>
        /// 评分标准小类
        /// </summary>
        public string SCClassifyName { get; set; }

        /// <summary>
        /// 评分标准检明细查项目
        /// </summary>
        public string SCNormProjectName { get; set; }

        /// <summary>
        /// 评分标准明细评分标准
        /// </summary>
        public string SCNormStandardName { get; set; }
        
        /// <summary>
        /// 扣分几处
        /// </summary>
        public int DeductionSeveral { get; set; }

        /// <summary>
        /// 扣分扣积分
        /// </summary>
        public int DeductionScore { get; set; }

        /// <summary>
        /// 扣分扣描述
        /// </summary>
        public string DeductionDescribe { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 创建用户用户名
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// 最后修改用户Id
        /// </summary>
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户名
        /// </summary>
        public string LastModifyUserName { get; set; }
    }
}
