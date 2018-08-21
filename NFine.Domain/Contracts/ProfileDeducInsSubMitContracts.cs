using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Contracts
{
    /// <summary>
    /// 扣分添加接受model
    /// </summary>
    public class ProfileDeducInsSubMitContracts
    {

        public string F_Id { get; set; }

        public string TaskEntryId { get; set; }

        public string NormId { get; set; }

        public int DeductionSeveral { get; set; }

        public int DeductionScore { get; set; }

        public string DeductionDescribe { get; set; }
    }
}
