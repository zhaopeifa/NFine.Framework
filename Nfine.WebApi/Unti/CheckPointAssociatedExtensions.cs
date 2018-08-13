using Nfine.WebApi.Data.Enums;
using NFine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nfine.WebApi.Unti
{
    /// <summary>
    /// 检查点关联 实际数据当中的关联与检查 点关联
    /// </summary>
    public class CheckPointAssociatedExtensions
    {
        public static int GetWayGradeAssociated(CheckPointClassificationEnum type)
        {
            int result = -1;
            switch (type)
            {
                case CheckPointClassificationEnum.特级道路:
                    result = (int)ProfileWayGradeEnum.一级道路;
                    break;
                case CheckPointClassificationEnum.一级道路:
                    result = (int)ProfileWayGradeEnum.一级道路;
                    break;
                case CheckPointClassificationEnum.二级道路:
                    result = (int)ProfileWayGradeEnum.二级道路;
                    break;
                case CheckPointClassificationEnum.三级道路:
                    result = (int)ProfileWayGradeEnum.三级及其它;
                    break;
                case CheckPointClassificationEnum.背街小巷:
                    result = (int)ProfileWayGradeEnum.三级及其它;
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}