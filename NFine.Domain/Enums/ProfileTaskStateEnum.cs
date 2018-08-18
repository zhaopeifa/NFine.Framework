using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Enums
{
    /// <summary>
    /// 环评-任务状态
    /// </summary>
    public enum ProfileTaskStateEnum
    {
        /// <summary>
        /// 未派遣
        /// </summary>
        NotToSend = 0,
        /// <summary>
        /// 待审核
        /// </summary>
        ToAudit = 1,
        /// <summary>
        /// 退回从新任务
        /// </summary>
        BackTo = 2,
        /// <summary>
        /// 已终结，审核完毕
        /// </summary>
        HavePutAnEndTo = 3,
        /// <summary>
        /// 已作废。
        /// </summary>
        TheCancellation = -1
    }

    public static class ProfileTaskStateEnumExtension
    {
        public static int GetIntValue(this ProfileTaskStateEnum type)
        {
            return (int)type;
        }
        public static string GetAnnotation(this ProfileTaskStateEnum type)
        {
            string result = string.Empty;
            switch (type)
            {
                case ProfileTaskStateEnum.NotToSend:
                    result = "未派遣";
                    break;
                case ProfileTaskStateEnum.ToAudit:
                    result = "待审核";
                    break;
                case ProfileTaskStateEnum.HavePutAnEndTo:
                    result = "已终结";
                    break;
                case ProfileTaskStateEnum.TheCancellation:
                    result = "已作废";
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
