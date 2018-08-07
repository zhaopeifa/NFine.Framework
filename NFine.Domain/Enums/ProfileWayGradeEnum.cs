using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Enums
{
    /// <summary>
    /// 巡检道路等级
    /// </summary>
    public enum ProfileWayGradeEnum
    {
        一级道路 = 1,
        二级道路,
        三级及其它,
    }

    public static class ProfileWayGradeEnumExtension
    {
        private static List<ProfileScoringClassifyEntryType> _scoringClassify;
        public static List<ProfileScoringClassifyEntryType> GetScoringClassify(this ProfileWayGradeEnum type)
        {
            if (_scoringClassify == null)
            {
                _scoringClassify = new List<ProfileScoringClassifyEntryType>();
            }
            else
            {
                _scoringClassify.Clear();
            }

            switch (type)
            {
                case ProfileWayGradeEnum.一级道路:
                    _scoringClassify.Add(ProfileScoringClassifyEntryType.特级道路);
                    _scoringClassify.Add(ProfileScoringClassifyEntryType.一级道路);
                    break;
                case ProfileWayGradeEnum.二级道路:
                    _scoringClassify.Add(ProfileScoringClassifyEntryType.二级道路);
                    break;
                case ProfileWayGradeEnum.三级及其它:
                    _scoringClassify.Add(ProfileScoringClassifyEntryType.三级道路);
                    _scoringClassify.Add(ProfileScoringClassifyEntryType.背街小巷);
                    break;
                default:
                    break;
            }

            return _scoringClassify;
        }
    }
}
