using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Enums
{
    /// <summary>
    /// 垃圾箱房类型
    /// </summary>
    public enum ProfileSanitationGarbageBoxTypeEnum
    {
        无 = 0,
        小区内 = 1,
        沿街 = 2
    }

    public static class ProfileSanitationGarbageBoxTypeEnumExtension
    {
        /// <summary>
        /// 获取枚举的对应的字符串值
        /// </summary>
        /// <returns></returns>
        public static int GetIntValue(this ProfileSanitationGarbageBoxTypeEnum DataViewType)
        {
            return ((int)DataViewType);
        }

        public static int GetGarbageBoxTypeEnumValue(this string GarbageBoxTypeName)
        {
            int result = 0;
            switch (GarbageBoxTypeName)
            {
                case "小区内":
                    result = (int)ProfileSanitationGarbageBoxTypeEnum.小区内;
                    break;
                case "沿街":
                    result = (int)ProfileSanitationGarbageBoxTypeEnum.沿街;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
