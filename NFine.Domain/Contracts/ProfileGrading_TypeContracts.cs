using NFine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Contracts
{
    /// <summary>
    /// 评分标准
    /// </summary>
    public class ProfileGrading_TypeContracts
    {
        private ProfileScoringClassifyEntryType[] _associatedClassifyTypes;

        public string F_Id { get; set; }
        public string Name { get; set; }
        public string AssociatedClassifyStr { get; set; }
        public ProfileScoringClassifyEntryType[] AssociatedClassifyTypes
        {
            get
            {
                if (_associatedClassifyTypes == null)
                {
                    if (!string.IsNullOrEmpty(AssociatedClassifyStr))
                    {
                        string[] associatedClassifyStrs = AssociatedClassifyStr.Split(',');

                        _associatedClassifyTypes = new ProfileScoringClassifyEntryType[associatedClassifyStrs.Length];

                        int typeKey = 0;
                        for (int i = 0; i < associatedClassifyStrs.Length; i++)
                        {
                            int.TryParse(associatedClassifyStrs[i], out typeKey);
                            _associatedClassifyTypes[i] = (ProfileScoringClassifyEntryType)typeKey;
                        }
                    }
                    else
                    {
                        _associatedClassifyTypes = new ProfileScoringClassifyEntryType[0];

                    }
                }
                return _associatedClassifyTypes;
            }
        }

        public string AssociatedClassifyFormaStr
        {
            get
            {
                StringBuilder str = new StringBuilder();
                if (AssociatedClassifyTypes == null || AssociatedClassifyTypes.Length <= 0)
                {
                    str.Append("无");
                }
                else
                {
                    for (int i = 0; i < AssociatedClassifyTypes.Length; i++)
                    {
                        str.Append(AssociatedClassifyTypes[i].ToString());
                        str.Append(",");
                    }
                }
                return str.ToString();
            }
        }
        public int Grade { get; set; }
    }
}
