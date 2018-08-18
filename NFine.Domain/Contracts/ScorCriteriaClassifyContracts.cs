using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Contracts
{
    /// <summary>
    /// 评分标准 小类
    /// </summary>
    public class ScorCriteriaClassifyContracts
    {
        private string _typeNameStr = string.Empty;

        public string groupId { get; set; }

        public string SClassifyName { get; set; }

        public int Score { get; set; }

        public string[] STypeIds { get; set; }

        public string[] STypeNames { get; set; }

        public string EntryName { get; set; }

        public string TypeNameStr
        {
            get
            {
                if (string.IsNullOrEmpty(_typeNameStr))
                {
                    StringBuilder str = new StringBuilder();
                    foreach (var item in STypeNames)
                    {
                        str.Append(item);
                        str.Append(",");
                    }
                    this._typeNameStr = str.ToString();
                }

                return _typeNameStr;
            }
        }
    }
}
