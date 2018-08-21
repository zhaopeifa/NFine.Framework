using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Contracts
{
    /// <summary>
    /// 获取评分标准
    /// </summary>
    public class ScireCriteriaContracts
    {
        public string SEntryId { get; set; }
        public string SEntryName { get; set; }

        public string STypeId { get; set; }

        public string STypeName { get; set; }

        public string SClassifyId { get; set; }

        public string SClassifyName { get; set; }

        public int SClassifyScore { get; set; }

        public List<ScireCriteriaNormContracts> SNromCollecion { get; set; }
    }

    /// <summary>
    /// 评分规则
    /// </summary>
    public class ScireCriteriaNormContracts
    {
        public string SNormId { get; set; }

        public string SNormProjectName { get; set; }

        public string SNormStandardName { get; set; }

        public int SNormCondition { get; set; }

    }
}
