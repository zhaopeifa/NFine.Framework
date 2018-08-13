using Nfine.WebApi.Contracts;
using Nfine.WebApi.Data.Enums;
using NFine.Application.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Nfine.WebApi.Code.CheckingPoint
{
    public class CheckingPointCode : ICheckingPoint
    {
        private ProfileProjectApp App = new ProfileProjectApp();
        public List<ApiCheckingPointContracts> GetCheckingPoint(string ProjectId)
        {
            List<ApiCheckingPointContracts> result = null;

            //根据项目Id获取当前项目
            string projectType = App.FildSql<string>(d => d.F_Id == ProjectId, d => d.ProjectType).FirstOrDefault();

            switch (projectType.ToLower())
            {
                case "sanitation"://环卫
                    result = ProjectType.Sanitation.GetProject_CheckPoint().
                        Select(d => new ApiCheckingPointContracts() { Code = d.GetIntValue(), Name = d.GetComments(), ProjectId = ProjectId }).ToList();
                    break;
                case "amenities": //市容
                    break;
                case "fivechaos": //五乱
                    break;
                default:
                    break;
            }

            return result;
        }

        public List<ApiCheckingPointClassification> GetCheckingPointClassification(CheckPointTypeEnum CheckPointCode)
        {
            var checkPointC = CheckPointCode.GetCheckPointClassification();

            return checkPointC.Select(d => new ApiCheckingPointClassification()
            {
                Name = d.ToString(),
                Code = d.GetIntValue(),
                CheckPointCode = CheckPointCode.GetIntValue()
            }).ToList();
        }
    }
}