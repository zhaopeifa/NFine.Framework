using NFine.Data.Extensions;
using NFine.Domain.Contracts;
using NFine.Domain.Entity.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.SystemManage
{
    public class ProfileDeducInsApp
    {
        private ProfileDeducInsRepository service = new ProfileDeducInsRepository();

        public void SubmitForm(ProfileDeducInsSubMitContracts entity, string keyValue)
        {
            service.SubmitForm(entity, keyValue);
        }

        public ProfileDeducInsSubMitContracts GetForm(string normId, string taskEntryId)
        {
            var deducInsQuery = LinqSQLExtensions.IQueryable<ProfileDeducInsEntity>().Where(d => d.SCNorm_Id == normId && d.TaskEntry_Id == taskEntryId);


            if (deducInsQuery.Count() > 0)
            {
                var deduInsEntiy = deducInsQuery.FirstOrDefault();
                return new ProfileDeducInsSubMitContracts()
                {
                    F_Id = deduInsEntiy.DeducIns_Id,
                    TaskEntryId = deduInsEntiy.TaskEntry_Id,
                    DeductionDescribe = deduInsEntiy.DeductionDescribe,
                    DeductionSeveral = deduInsEntiy.DeductionSeveral,
                    DeductionScore = deduInsEntiy.DeductionScore,
                    NormId = deduInsEntiy.SCNorm_Id
                };
            }

            return null;
        }
    }
}
