using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nfine.WebApi.Data.Enums
{
    public enum ProjectType
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
    public enum CheckPointTypeEnum
    {
        /// <summary>
        /// 道路
        /// </summary>
        way = 1,
        /// <summary>
        /// 环卫公厕
        /// </summary>
        Tandas = 2,
        /// <summary>
        /// 污水池
        /// </summary>
        cesspit = 3,
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
        GreenResidentials,
        /// <summary>
        /// 环卫作业车辆
        /// </summary>
        Sanitationvehicle,
        /// <summary>
        /// 机动车
        /// </summary>
        AMotorVehicle
    }

    public enum CheckPointClassificationEnum
    {
        特级道路 = 1,
        一级道路,
        二级道路,
        三级道路,
        背街小巷,

        沿街箱房,
        非沿街箱房,

        沿街压缩站,
        非沿街压缩站,

        一类公厕,
        二类公厕,
        三类公厕,

        倒粪站小便池,

        机扫车,
        冲洗车,
        清运车,
        电动机具,

        绿化带,

        绿色账户小区
    }
    public static class CheckPointTypeEnumExtension
    {
        public static int GetIntValue(this CheckPointTypeEnum type)
        {
            return (int)type;
        }

        public static string GetComments(this CheckPointTypeEnum type)
        {
            string result = string.Empty;
            switch (type)
            {
                case CheckPointTypeEnum.way:
                    result = "道路清扫";
                    break;
                case CheckPointTypeEnum.Tandas:
                    result = "环卫公厕";
                    break;
                case CheckPointTypeEnum.cesspit:
                    result = "倒粪站小便池";
                    break;
                case CheckPointTypeEnum.GarbageBox:
                    result = "垃圾箱房";
                    break;
                case CheckPointTypeEnum.compressionStation:
                    result = "小压站";
                    break;
                case CheckPointTypeEnum.Greening:
                    result = "沿途绿化";
                    break;
                case CheckPointTypeEnum.GreenResidentials:
                    result = "绿色账户小区";
                    break;
                case CheckPointTypeEnum.Sanitationvehicle:
                    result = "环卫作业车辆";
                    break;
                case CheckPointTypeEnum.AMotorVehicle:
                    result = "机动车";
                    break;
                default:
                    break;
            }

            return result;
        }

        public static int GetIntValue(this CheckPointClassificationEnum type)
        {
            return (int)type;
        }

        public static int GetIntValue(this ProjectType type)
        {
            return (int)type;
        }

        public static CheckPointTypeEnum[] GetProject_CheckPoint(this ProjectType type)
        {
            CheckPointTypeEnum[] result = null;
            switch (type)
            {
                case ProjectType.Sanitation:
                    result = new CheckPointTypeEnum[9];

                    result[0] = CheckPointTypeEnum.way;
                    result[1] = CheckPointTypeEnum.Tandas;
                    result[2] = CheckPointTypeEnum.cesspit;
                    result[3] = CheckPointTypeEnum.GarbageBox;
                    result[4] = CheckPointTypeEnum.compressionStation;
                    result[5] = CheckPointTypeEnum.Greening;
                    result[6] = CheckPointTypeEnum.GreenResidentials;
                    result[7] = CheckPointTypeEnum.Sanitationvehicle;
                    result[8] = CheckPointTypeEnum.AMotorVehicle;
                    break;
                case ProjectType.Amenities:
                    break;
                case ProjectType.FiveChaos:
                    break;
                default:
                    break;
            }


            return result;
        }

        public static CheckPointClassificationEnum[] GetCheckPointClassification(this CheckPointTypeEnum type)
        {
            CheckPointClassificationEnum[] result = null;

            switch (type)
            {
                case CheckPointTypeEnum.way:
                    result = new CheckPointClassificationEnum[5];

                    result[0] = CheckPointClassificationEnum.特级道路;
                    result[1] = CheckPointClassificationEnum.一级道路;
                    result[2] = CheckPointClassificationEnum.二级道路;
                    result[3] = CheckPointClassificationEnum.三级道路;
                    result[4] = CheckPointClassificationEnum.背街小巷;

                    break;
                case CheckPointTypeEnum.Tandas:
                    result = new CheckPointClassificationEnum[3];

                    result[0] = CheckPointClassificationEnum.一类公厕;
                    result[1] = CheckPointClassificationEnum.二类公厕;
                    result[2] = CheckPointClassificationEnum.三类公厕;

                    break;
                case CheckPointTypeEnum.cesspit:
                    result = new CheckPointClassificationEnum[1];

                    result[0] = CheckPointClassificationEnum.倒粪站小便池;
                    break;
                case CheckPointTypeEnum.GarbageBox:
                    result = new CheckPointClassificationEnum[2];

                    result[0] = CheckPointClassificationEnum.沿街箱房;
                    result[1] = CheckPointClassificationEnum.非沿街箱房;
                    break;
                case CheckPointTypeEnum.compressionStation:
                    result = new CheckPointClassificationEnum[2];

                    result[0] = CheckPointClassificationEnum.沿街压缩站;
                    result[1] = CheckPointClassificationEnum.非沿街压缩站;
                    break;
                case CheckPointTypeEnum.Greening:
                    result = new CheckPointClassificationEnum[1];

                    result[0] = CheckPointClassificationEnum.绿化带;
                    break;
                case CheckPointTypeEnum.GreenResidentials:
                    result = new CheckPointClassificationEnum[1];

                    result[0] = CheckPointClassificationEnum.绿色账户小区;
                    break;
                case CheckPointTypeEnum.Sanitationvehicle:
                    result = new CheckPointClassificationEnum[1];

                    result[0] = CheckPointClassificationEnum.电动机具;
                    break;
                case CheckPointTypeEnum.AMotorVehicle:
                    result = new CheckPointClassificationEnum[3];

                    result[0] = CheckPointClassificationEnum.机扫车;
                    result[1] = CheckPointClassificationEnum.冲洗车;
                    result[2] = CheckPointClassificationEnum.清运车;
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}