/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace NFine.Web.Controllers
{
    [HandlerLogin]
    public class ClientsDataController : Controller
    {
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetClientsDataJson()
        {
            var data = new
            {
                dataItems = this.GetDataItemList(),
                organize = this.GetOrganizeList(),
                role = this.GetRoleList(),
                duty = this.GetDutyList(),
                user = "",
                profileCity = this.GetProfileCityList(),
                authorizeMenu = this.GetMenuList(),
                authorizeButton = this.GetMenuButtonList(),
                ProfileOperationsCompany = this.GetProfileOperationsCompany(),
                profileCounty = this.GetProfileCounty(),
                profileStreet = this.GetProfileStreet(),
                ProfileWayGrades = this.GetProfileWayGrades(),
                ProfileTandasGrade = this.GetProfileTandasGrade(),
                ProfileTandasManagementForms = this.GetProfileTandasManagementForms(),
                ProfileSanitationGarbageBoxType = this.GetProfileSanitationGarbageBoxType(),
                ProfileWatersType = this.GetProfileWatersType(),
                ProfileGovernSort = this.GetProfileGovernSort(),
                ProfileEnvironmentType = this.GetProfileEnvironmentType(),
                ProfileGovernType = this.GetProfileGovernType()
            };
            return Content(data.ToJson());
        }
        private object GetDataItemList()
        {
            var itemdata = new ItemsDetailApp().GetList();
            Dictionary<string, object> dictionaryItem = new Dictionary<string, object>();
            foreach (var item in new ItemsApp().GetList())
            {
                var dataItemList = itemdata.FindAll(t => t.F_ItemId.Equals(item.F_Id));
                Dictionary<string, string> dictionaryItemList = new Dictionary<string, string>();
                foreach (var itemList in dataItemList)
                {
                    dictionaryItemList.Add(itemList.F_ItemCode, itemList.F_ItemName);
                }
                dictionaryItem.Add(item.F_EnCode, dictionaryItemList);
            }
            return dictionaryItem;
        }
        private object GetOrganizeList()
        {
            OrganizeApp organizeApp = new OrganizeApp();
            var data = organizeApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (OrganizeEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_EnCode,
                    fullname = item.F_FullName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }
        private object GetRoleList()
        {
            RoleApp roleApp = new RoleApp();
            var data = roleApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (RoleEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_EnCode,
                    fullname = item.F_FullName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }
        private object GetDutyList()
        {
            DutyApp dutyApp = new DutyApp();
            var data = dutyApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (RoleEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_EnCode,
                    fullname = item.F_FullName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }
        private object GetMenuList()
        {
            var roleId = OperatorProvider.Provider.GetCurrent().RoleId;
            return ToMenuJson(new RoleAuthorizeApp().GetEnableMenuList(roleId), "0");
        }
        private string ToMenuJson(List<ModuleEntity> data, string parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("[");
            List<ModuleEntity> entitys = data.FindAll(t => t.F_ParentId == parentId);
            if (entitys.Count > 0)
            {
                foreach (var item in entitys)
                {
                    string strJson = item.ToJson();
                    strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + ToMenuJson(data, item.F_Id) + "");
                    sbJson.Append(strJson + ",");
                }
                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append("]");
            return sbJson.ToString();
        }
        private object GetMenuButtonList()
        {
            var roleId = OperatorProvider.Provider.GetCurrent().RoleId;
            var data = new RoleAuthorizeApp().GetButtonList(roleId);
            var dataModuleId = data.Distinct(new ExtList<ModuleButtonEntity>("F_ModuleId"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (ModuleButtonEntity item in dataModuleId)
            {
                var buttonList = data.Where(t => t.F_ModuleId.Equals(item.F_ModuleId));
                dictionary.Add(item.F_ModuleId, buttonList);
            }
            return dictionary;
        }

        private object GetProfileCityList()
        {
            ProfileCityApp cityApp = new ProfileCityApp();
            var data = cityApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (ProfileCityEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_Id,
                    fullname = item.CityName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }

        private object GetProfileOperationsCompany()
        {
            ProfileOperationsCompanyApp cityApp = new ProfileOperationsCompanyApp();
            var data = cityApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (ProfileOperationsCompanyEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_Id,
                    fullname = item.CompanyName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }

        private object GetProfileCounty()
        {
            ProfileCountyApp countyApp = new ProfileCountyApp();
            var data = countyApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (ProfileCountyEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_Id,
                    fullname = item.CountyName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }

        public object GetProfileStreet()
        {
            ProfileStreetApp streetApp = new ProfileStreetApp();
            var data = streetApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (ProfileStreetEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_Id,
                    fullname = item.StreetName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }

        public object GetProfileWayGrades()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (int myCode in System.Enum.GetValues(typeof(NFine.Domain.Enums.ProfileWayGradeEnum)))
            {
                string strName = System.Enum.GetName(typeof(NFine.Domain.Enums.ProfileWayGradeEnum), myCode);//获取名称

                var fieldItem = new
                {
                    encode = myCode,
                    fullname = strName
                };
                dictionary.Add(myCode.ToString(), fieldItem);
            }
            return dictionary;

        }

        public object GetProfileTandasGrade()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (int myCode in System.Enum.GetValues(typeof(NFine.Domain.Enums.ProfileTandasGradeEnum)))
            {
                string strName = System.Enum.GetName(typeof(NFine.Domain.Enums.ProfileTandasGradeEnum), myCode);//获取名称

                var fieldItem = new
                {
                    encode = myCode,
                    fullname = strName
                };
                dictionary.Add(myCode.ToString(), fieldItem);
            }
            return dictionary;
        }

        public object GetProfileTandasManagementForms()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (int myCode in System.Enum.GetValues(typeof(NFine.Domain.Enums.ProfileTandasManagementFormEnum)))
            {
                string strName = System.Enum.GetName(typeof(NFine.Domain.Enums.ProfileTandasManagementFormEnum), myCode);//获取名称

                var fieldItem = new
                {
                    encode = myCode,
                    fullname = strName
                };
                dictionary.Add(myCode.ToString(), fieldItem);
            }
            return dictionary;
        }

        public object GetProfileSanitationGarbageBoxType()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (int myCode in System.Enum.GetValues(typeof(NFine.Domain.Enums.ProfileSanitationGarbageBoxTypeEnum)))
            {
                string strName = System.Enum.GetName(typeof(NFine.Domain.Enums.ProfileSanitationGarbageBoxTypeEnum), myCode);//获取名称

                var fieldItem = new
                {
                    encode = myCode,
                    fullname = strName
                };
                dictionary.Add(myCode.ToString(), fieldItem);
            }
            return dictionary;
        }

        public object GetProfileWatersType()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (int myCode in System.Enum.GetValues(typeof(NFine.Domain.Enums.ProfileWatersTypeEnum)))
            {
                string strName = System.Enum.GetName(typeof(NFine.Domain.Enums.ProfileWatersTypeEnum), myCode);//获取名称

                var fieldItem = new
                {
                    encode = myCode,
                    fullname = strName
                };
                dictionary.Add(myCode.ToString(), fieldItem);
            }
            return dictionary;
        }

        public object GetProfileGovernSort()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (int myCode in System.Enum.GetValues(typeof(NFine.Domain.Enums.ProfileGovernSortEnum)))
            {
                string strName = System.Enum.GetName(typeof(NFine.Domain.Enums.ProfileGovernSortEnum), myCode);//获取名称

                var fieldItem = new
                {
                    encode = myCode,
                    fullname = strName
                };
                dictionary.Add(myCode.ToString(), fieldItem);
            }
            return dictionary;
        }

        public object GetProfileEnvironmentType()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (int myCode in System.Enum.GetValues(typeof(NFine.Domain.Enums.ProfileEnvironmentTypeEnum)))
            {
                string strName = System.Enum.GetName(typeof(NFine.Domain.Enums.ProfileEnvironmentTypeEnum), myCode);//获取名称

                var fieldItem = new
                {
                    encode = myCode,
                    fullname = strName
                };
                dictionary.Add(myCode.ToString(), fieldItem);
            }
            return dictionary;
        }

        public object GetProfileGovernType()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (int myCode in System.Enum.GetValues(typeof(NFine.Domain.Enums.ProfileGovernTypeEnum)))
            {
                string strName = System.Enum.GetName(typeof(NFine.Domain.Enums.ProfileGovernTypeEnum), myCode);//获取名称

                var fieldItem = new
                {
                    encode = myCode,
                    fullname = strName
                };
                dictionary.Add(myCode.ToString(), fieldItem);
            }
            return dictionary;
        }
    }
}
