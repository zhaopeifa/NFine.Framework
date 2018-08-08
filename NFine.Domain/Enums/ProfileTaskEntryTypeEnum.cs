﻿using System;
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
        GarbageBox
    }


    public static class ProfileTaskEntryTypeEnumExtension
    {
        public static int GetIntValue(this ProfileTaskEntryTypeEnum type)
        {
            return (int)type;
        }
    }
}
