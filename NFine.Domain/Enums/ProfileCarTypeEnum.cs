using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Enums
{
    /// <summary>
    /// 车辆类型
    /// </summary>
    public enum ProfileCarTypeEnum
    {
        /// <summary>
        /// 机扫车
        /// </summary>
        MachineCleanCar = 1,
        /// <summary>
        /// 冲洗车
        /// </summary>
        WashTheCar = 2,
        /// <summary>
        /// 垃圾清运车
        /// </summary>
        GarbageTruck = 3,
        /// <summary>
        /// 飞行保洁车
        /// </summary>
        FlyingCar = 4,
        /// <summary>
        /// 四轮八桶车
        /// </summary>
        EightLadleCar=5
    }
    public static class ProfileCarTypeEnumExtension
    {
        public static int GetIntValue(this ProfileCarTypeEnum type)
        {
            return (int)type;
        }
    }
}
