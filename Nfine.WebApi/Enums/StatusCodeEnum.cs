using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nfine.WebApi.Enums
{
    /// <summary>
    /// 返回状态
    /// </summary>
    public enum StatusCodeEnum
    {
        成功 = 0,
        失败=-1,
        系统错误=500
    }

    public static class StatusCodeEnumExtension
    {
        /// <summary>
        /// 获取枚举的对应的字符串值
        /// </summary>
        /// <returns></returns>
        public static int GetIntValue(this StatusCodeEnum DataViewType)
        {
            return ((int)DataViewType);
        }
    }
}