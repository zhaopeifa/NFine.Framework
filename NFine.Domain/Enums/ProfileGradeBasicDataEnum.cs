using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Enums
{
    /// <summary>
    /// 评分标准大类
    /// </summary>
    public enum ProfileGradeBasicDataEnum
    {
        道路 = 1,
        垃圾箱房 = 2,
        压缩站 = 3,
        公厕 = 4,
        沿街垃圾收集设施 = 5,
        环卫车辆 = 6,
        绿化 = 7,
        绿色账户小区 = 8
    }

    public enum ProfileScoringClassifyEntryType
    {
        特级道路 = 1,
        一级道路,
        二级道路,
        三级道路,
        背街小巷,

        沿街箱房,
        非沿街箱房,

        沿街压缩站,
        非沿街压缩站,

        一类公厕,
        二类公厕,
        三类公厕,

        废物箱,
        沿街垃圾桶,
        倒粪站小便池,

        机扫车,
        冲洗车,
        清运车,
        电动机具,

        绿化带,

        绿色账户小区

    }

    public static class ProfileGradeEnumExtension
    {
        private static List<ProfileScoringClassifyEntryType> _scoringClassify;
        private static List<ProfileScoringClassifyEntryType> _gradeAssociatedEntryList;
        private static List<ProfileGradeBasicDataEnum> _sanitationGrade;

        private static List<ProfileScoringClassifyEntryType> scoringClassify { get { return _scoringClassify ?? (_scoringClassify = new List<ProfileScoringClassifyEntryType>()); } }
        public static List<ProfileGradeBasicDataEnum> SanitationGrade
        {
            get
            {
                if (_sanitationGrade == null)
                {
                    _sanitationGrade = new List<ProfileGradeBasicDataEnum>();

                    _sanitationGrade.Add(ProfileGradeBasicDataEnum.道路);
                    _sanitationGrade.Add(ProfileGradeBasicDataEnum.垃圾箱房);
                    _sanitationGrade.Add(ProfileGradeBasicDataEnum.压缩站);
                    _sanitationGrade.Add(ProfileGradeBasicDataEnum.沿街垃圾收集设施);
                    _sanitationGrade.Add(ProfileGradeBasicDataEnum.环卫车辆);
                    _sanitationGrade.Add(ProfileGradeBasicDataEnum.绿化);
                    _sanitationGrade.Add(ProfileGradeBasicDataEnum.绿色账户小区);
                }

                return _sanitationGrade;
            }
        }


        public static List<ProfileScoringClassifyEntryType> GetGradeType(ProfileGradeBasicDataEnum baseData)
        {
            if (_gradeAssociatedEntryList == null)
            {
                _gradeAssociatedEntryList = new List<ProfileScoringClassifyEntryType>();
            }
            _gradeAssociatedEntryList.Clear();

            switch (baseData)
            {
                case ProfileGradeBasicDataEnum.道路:
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.特级道路);
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.一级道路);
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.二级道路);
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.三级道路);
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.背街小巷);
                    break;
                case ProfileGradeBasicDataEnum.垃圾箱房:
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.沿街箱房);
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.非沿街箱房);
                    break;
                case ProfileGradeBasicDataEnum.压缩站:
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.沿街压缩站);
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.非沿街压缩站);
                    break;
                case ProfileGradeBasicDataEnum.公厕:
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.一类公厕);
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.二类公厕);
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.三类公厕);
                    break;
                case ProfileGradeBasicDataEnum.沿街垃圾收集设施:
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.废物箱);
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.沿街垃圾桶);
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.倒粪站小便池);
                    break;
                case ProfileGradeBasicDataEnum.环卫车辆:
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.机扫车);
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.冲洗车);
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.清运车);
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.电动机具);
                    break;
                case ProfileGradeBasicDataEnum.绿化:
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.绿化带);
                    break;
                case ProfileGradeBasicDataEnum.绿色账户小区:
                    _gradeAssociatedEntryList.Add(ProfileScoringClassifyEntryType.绿色账户小区);
                    break;
                default:
                    break;
            }

            return _gradeAssociatedEntryList;
        }

        #region 获取关联
        /// <summary>
        /// 获取道路等级关联的
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<ProfileScoringClassifyEntryType> GetWayGradeScoringClassify(ProfileWayGradeEnum type)
        {
            scoringClassify.Clear();

            switch (type)
            {
                case ProfileWayGradeEnum.一级道路:
                    scoringClassify.Add(ProfileScoringClassifyEntryType.特级道路);
                    scoringClassify.Add(ProfileScoringClassifyEntryType.一级道路);
                    break;
                case ProfileWayGradeEnum.二级道路:
                    _scoringClassify.Add(ProfileScoringClassifyEntryType.二级道路);
                    break;
                case ProfileWayGradeEnum.三级及其它:
                    scoringClassify.Add(ProfileScoringClassifyEntryType.三级道路);
                    scoringClassify.Add(ProfileScoringClassifyEntryType.背街小巷);
                    break;
                default:
                    break;
            }

            return _scoringClassify;
        }

        public static List<ProfileScoringClassifyEntryType> GetGarbageBox(ProfileSanitationGarbageBoxTypeEnum type)
        {
            scoringClassify.Clear();
            switch (type)
            {
                case ProfileSanitationGarbageBoxTypeEnum.无:
                    scoringClassify.Add(ProfileScoringClassifyEntryType.非沿街箱房);
                    break;
                case ProfileSanitationGarbageBoxTypeEnum.小区内:
                    scoringClassify.Add(ProfileScoringClassifyEntryType.非沿街箱房);
                    break;
                case ProfileSanitationGarbageBoxTypeEnum.沿街:
                    scoringClassify.Add(ProfileScoringClassifyEntryType.沿街箱房);
                    break;
                default:
                    break;
            }
            return scoringClassify;
        }
        #endregion
    }
}
