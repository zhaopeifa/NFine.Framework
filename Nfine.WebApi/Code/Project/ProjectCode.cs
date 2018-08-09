using NFine.Application.SystemManage;
using NFine.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Nfine.WebApi.Code.Project
{
    public class ProjectCode : IProject
    {
        private ProfileProjectApp ProfileCountyApp = new ProfileProjectApp();

        public List<NFine.Domain.Contracts.ApiProjectContracts> GetProject(string countyId = "")
        {
            StringBuilder sqlStr = new StringBuilder();

            sqlStr.Append("SELECT * FROM ProfileProject WHERE 1=1 ");

            if (!string.IsNullOrEmpty(countyId))
            {
                sqlStr.Append(" and CountyId='" + countyId + "'");
            }

            return ProfileCountyApp.FildSql(sqlStr.ToString()).Select(d => new ApiProjectContracts()
            {
                Id = d.F_Id,
                CountyId = d.CountyId,
                Name = d.ProjectName
            }).ToList();
        }
    }
}