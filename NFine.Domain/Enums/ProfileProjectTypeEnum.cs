using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Enums
{
    /// <summary>
    /// 项目类型
    /// </summary>
    public enum ProfileProjectTypeEnum
    {
        /// <summary>
        /// 环卫
        /// </summary>
        Sanitation = 1,
        /// <summary>
        /// 市容
        /// </summary>
        Amenities = 2,
        /// <summary>
        /// 五乱
        /// </summary>
        FiveChaos = 3
    }

    public static class ProfileProjectTypeEnumExtension
    {
        public static int GetIntValue(this ProfileProjectTypeEnum DataViewType)
        {
            return (int)DataViewType;
        }
        public static string GetDescribe(this ProfileProjectTypeEnum DataViewType)
        {
            string result = string.Empty;
            switch (DataViewType)
            {
                case ProfileProjectTypeEnum.Sanitation:
                    result = "城市环卫";
                    break;
                case ProfileProjectTypeEnum.Amenities:
                    result = "城市市容";
                    break;
                case ProfileProjectTypeEnum.FiveChaos:
                    result = "城市五乱";
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
