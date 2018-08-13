using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Enums
{
    /// <summary>
    /// 任务项类型
    /// </summary>
    public enum ProfileTaskEntryTypeEnum
    {
        /// <summary>
        /// 道路
        /// </summary>
        Way = 1,
        /// <summary>
        /// 公厕
        /// </summary>
        Tandas,
        /// <summary>
        /// 垃圾厢房
        /// </summary>
        GarbageBox,
        /// <summary>
        /// 压缩站
        /// </summary>
        compressionStation,
        /// <summary>
        /// 沿途绿化
        /// </summary>
        Greening,
        /// <summary>
        /// 绿色账户小区
        /// </summary>
        GreenResidential
    }


    public static class ProfileTaskEntryTypeEnumExtension
    {
        public static int GetIntValue(this ProfileTaskEntryTypeEnum type)
        {
            return (int)type;
        }
    }
}
