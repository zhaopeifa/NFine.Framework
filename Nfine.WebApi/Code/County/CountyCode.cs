using Nfine.WebApi.Contracts;
using NFine.Application.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nfine.WebApi.Code.County
{
    public class CountyCode : ICounty
    {
        private ProfileCountyApp ProfileCountyApp = new ProfileCountyApp();
        public List<ApiProfileCountyContracts> GetProfileCountyEntitys(string cityId)
        {
            string sql = "SELECT * FROM ProfileCounty where 1=1 ";
            if (!string.IsNullOrEmpty(cityId))
            {
                sql += " and CityId='" + cityId + "'";
            }
            var result = ProfileCountyApp.FildSql(sql).Select(d => new ApiProfileCountyContracts() { Id = d.F_Id, Name = d.CountyName }).ToList();
            return result;
        }
    }
}