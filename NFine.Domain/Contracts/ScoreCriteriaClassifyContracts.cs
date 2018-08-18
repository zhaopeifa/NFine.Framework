using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Contracts
{
    public class ScoreCriteriaClassifyContracts
    {
        public string SClassifyId { get; set; }

        public string SClassifyName { get; set; }

        public string Score { get; set; }
        public string STypeId { get; set; }

        public string STypeIdName { get; set; }

        public string SEntryId { get; set; }
        public string SEntryName { get; set; }
    }
}
