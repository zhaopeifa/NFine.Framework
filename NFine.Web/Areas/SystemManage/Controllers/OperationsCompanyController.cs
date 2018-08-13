using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class OperationsCompanyController : ControllerBase
    {
        private ProfileOperationsCompanyApp companycleApp = new ProfileOperationsCompanyApp();
        private ProfileOperationsVehicleTypeApp vehucleTypeApp = new ProfileOperationsVehicleTypeApp();
        private UserApp userApp = new UserApp();

        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = companycleApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetVehicleTypeTree(string key)
        {
            var vehucleTypeData = vehucleTypeApp.GetList();
            ProfileOperationsCompanyEntity model = null;
            if (!string.IsNullOrEmpty(key))
            {
                model = companycleApp.GetForm(key);
            }

            var treeList = new List<TreeViewModel>();
            TreeViewModel tree = null;
            foreach (var item in vehucleTypeData)
            {
                tree = new TreeViewModel();
                bool hasChildren = vehucleTypeData.Count(t => t.VehicleTypeParentId == item.F_Id) == 0 ? false : true;
                bool isCheck = false;

                if (model != null &&
                    !string.IsNullOrEmpty(model.HasVehicleTypeIds))
                {
                    string[] vehicleTypes = model.HasVehicleTypeIds.Split(',');
                    isCheck = vehicleTypes.Contains(item.F_Id);
                }

                tree.id = item.F_Id;
                tree.text = item.VehicleTypeName;
                tree.parentId = item.VehicleTypeParentId == null ? "0" : item.VehicleTypeParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = isCheck ? 1 : 0;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }

            return Content(treeList.TreeViewJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableCompanyTypeGridJson()
        {

            List<object> list = new List<object>();

            foreach (int myCode in Enum.GetValues(typeof(NFine.Domain.Enums.ProfileOperationsCompanyTypeEnum)))
            {
                string strName = Enum.GetName(typeof(NFine.Domain.Enums.ProfileOperationsCompanyTypeEnum), myCode);//获取名称

                list.Add(new { id = myCode, text = strName });
            }
            return Content(list.ToJson());
        }

        /// <summary>
        /// 获取负责街道的环卫公司
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCompanyTypeJDEnableGridJson(string keyword)
        {
            //负责街道 只有街道的
            var data = companycleApp.GetDictionary(d => d.CompanyType == (int)ProfileOperationsCompanyTypeEnum.特定街道);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取负责车辆的环卫公司
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCompanyTypeCLEnableGridJson()
        {
            var data = companycleApp.GetDictionary(d => d.CompanyType == (int)ProfileOperationsCompanyTypeEnum.特定车辆);
            return Content(data.ToJson());
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = companycleApp.GetForm(keyValue);
            if (data.F_LastModifyUserId != null)
            {
                var data1 = userApp.GetForm(data.F_LastModifyUserId);

                if (data1 != null)
                {
                    data.F_LastModifyUserId = data1.F_RealName;
                }
            }
            if (data.F_CreatorUserId != null)
            {
                var data2 = userApp.GetForm(data.F_CreatorUserId);
                if (data2 != null)
                {
                    data.F_CreatorUserId = data2.F_RealName;
                }

            }
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            companycleApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProfileOperationsCompanyEntity operaEntity, string vehicleTypeIds, string keyValue)
        {
            operaEntity.HasVehicleTypeIds = vehicleTypeIds;

            companycleApp.SubmitForm(operaEntity, keyValue);
            return Success("操作成功。");
        }
    }
}