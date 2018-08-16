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
        GreenResidential,
        /// <summary>
        /// 倒粪池小便池
        /// </summary>
        cesspool,
        /// <summary>
        /// 废纸箱
        /// </summary>
        Wastebasket,
        /// <summary>
        /// 沿街垃圾桶
        /// </summary>
        StreetTrash,
        /// <summary>
        /// 机扫车
        /// </summary>
        MachineCleanCar,
        /// <summary>
        /// 冲洗车
        /// </summary>
        WashTheCar,
        /// <summary>
        /// 垃圾清运车
        /// </summary>
        GarbageTruckCar,
        /// <summary>
        /// 飞行保洁车
        /// </summary>
        FlyingCar,
        /// <summary>
        /// 四轮八桶车
        /// </summary>
        EightLadleCar
    }


    public static class ProfileTaskEntryTypeEnumExtension
    {
        public static int GetIntValue(this ProfileTaskEntryTypeEnum type)
        {
            return (int)type;
        }
    }
}
