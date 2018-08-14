using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Enums
{
    public enum ProfileAlongLitterBinTypeEnum
    {
        /// <summary>
        /// 废物箱
        /// </summary>
        LitterBin=1,
        /// <summary>
        /// 沿街垃圾桶
        /// </summary>
        StreetTrash=2
    }

    public static class ProfileAlongLitterBinTypeEnumExtension
    {
        public static int GetIntValue(this ProfileAlongLitterBinTypeEnum type)
        {
            return (int)type;
        }
    }
}
