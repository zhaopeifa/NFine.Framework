using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Contracts
{
    /// <summary>
    /// 任务派发前后传递数据Model
    /// </summary>
    public class TaskInsertContracts
    {
        public string F_Id { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string CityId { get; set; }
        /// <summary>
        /// 区县
        /// </summary>
        public string CountyId { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 街道Id
        /// </summary>
        public string StreetId { get; set; }

        /// <summary>
        /// 主路Id
        /// </summary>
        public string MainWayId { get; set; }

        public string WayId { get; set; }

        public int WayPlaceCount { get; set; }

        /// <summary>
        /// 环卫公厕数量
        /// </summary>
        public int TandasCount { get; set; }

        /// <summary>
        /// 垃圾箱房
        /// </summary>
        public int GarbageBoxCount { get; set; }

        /// <summary>
        /// 倒粪池数量
        /// </summary>
        public int CesspoolCount { get; set; }

        /// <summary>
        /// 小压站数量
        /// </summary>
        public int CompressionCount { get; set; }

        /// <summary>
        /// 沿途绿化数量
        /// </summary>
        public int GreeningCount { get; set; }

        /// <summary>
        /// 绿色账户小区数量
        /// </summary>
        public int GreenResidentialCount { get; set; }

        /// <summary>
        /// 废纸箱
        /// </summary>
        public int WastebasketCount { get; set; }

        /// <summary>
        /// 沿途垃圾箱
        /// </summary>
        public int StreetTrashCount { get; set; }

        /// <summary>
        /// 机扫车
        /// </summary>
        public int MachineCleanCarCount { get; set; }

        /// <summary>
        /// 冲洗车
        /// </summary>
        public int WashTheCarCount { get; set; }

        /// <summary>
        /// 垃圾清运车
        /// </summary>
        public int GarbageTruckCarCount { get; set; }

        /// <summary>
        /// 飞行保洁车
        /// </summary>
        public int FlyingCarCount { get; set; }

        /// <summary>
        /// 四轮八桶车
        /// </summary>
        public int EightLadleCarCount { get; set; }


        public string PersonInChargeId { get; set; }

        public DateTime DeliveryTime { get; set; }

        public DateTime CompletionTime { get; set; }
    }
}
