using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Code.Web;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class SanitationGarbageBoxController : ControllerBase
    {
        private ProfileSanitationGarbageBoxApp App = new ProfileSanitationGarbageBoxApp();
        private ProfileStreetApp StreetApp = new ProfileStreetApp();
        private UserApp userApp = new UserApp();


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, string cityId, string countyId, string projectId, string streetId)
        {
            var data = new
            {
                rows = App.GetList(pagination, keyword, projectId, streetId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProfileSanitationGarbageBoxEntity WayEntity, string keyValue)
        {
            App.SubmitForm(WayEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            App.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = App.GetForm(keyValue);
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


        /// <summary>
        /// 获取道路等级
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableGarbageBoxTypeGridJson()
        {
            List<object> list = new List<object>();

            foreach (int myCode in Enum.GetValues(typeof(NFine.Domain.Enums.ProfileSanitationGarbageBoxTypeEnum)))
            {
                string strName = Enum.GetName(typeof(NFine.Domain.Enums.ProfileSanitationGarbageBoxTypeEnum), myCode);//获取名称

                list.Add(new { id = myCode, text = strName });
            }
            return Content(list.ToJson());
        }

        [HttpGet]
        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SummitImport(string CityId, string CountyId, string ProjectId, int isRename = 1)
        {
            var file = Request.Files[0];

            string path = @"D:\项目\聚力环境测评系统\NFine.Web\bin\TemporaryFilesDiskPath\";
            string fileName = file.FileName;
            string filePath = Path.Combine(path, fileName);

            ImportResultModel result = new ImportResultModel();
            int failureQuantity, successfulQuantity;

            try
            {
                if (NFine.Code.FileHelper.IsExistFile(path))
                {
                    NFine.Code.FileHelper.DeleteFile(path);
                }

                file.SaveAs(filePath);

                using (ExcelHelper exHelp = new ExcelHelper(filePath))
                {
                    var datatable = exHelp.ExcelToDataTable(filePath, true);

                    ProfileSanitationGarbageBoxEntity[] models = new ProfileSanitationGarbageBoxEntity[datatable.Rows.Count];

                    ProfileSanitationGarbageBoxEntity model;
                    for (int i = 0; i < datatable.Rows.Count; i++)
                    {
                        model = new ProfileSanitationGarbageBoxEntity();

                        var code = datatable.Rows[i]["序"].ToString();
                        var streetName = datatable.Rows[i]["街道"].ToString();
                        var garbageBoxTypeName = datatable.Rows[i]["类型"].ToString();
                        var address = datatable.Rows[i]["地址"].ToString();

                        string streetKey = "";

                        var streetList = StreetApp.GetDictionary(d => d.StreetName == streetName);
                        if (streetList.Count > 0)
                        {
                            streetKey = streetList[0].Key;
                        }
                        else {
                            continue;
                        }

                        model.F_EnCode = code;
                        model.CityId = CityId;
                        model.CountyId = CountyId;
                        model.ProjectId = ProjectId;
                        model.StreetId = streetKey;
                        model.GarbageBoxType = garbageBoxTypeName.GetGarbageBoxTypeEnumValue();
                        model.Address = address;

                        models[i] = model;
                    }

                    App.ImportData(models, out successfulQuantity, out failureQuantity);
                    result.IsSucceed = true;
                    result.FailureQuantity = failureQuantity;
                    result.SuccessfulQuantity = successfulQuantity;
                    result.TotalQuantity = models.Length;
                }

            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.ErrorMessage = ex.ToString();
            }
            finally
            {
                if (NFine.Code.FileHelper.IsExistFile(path))
                {
                    NFine.Code.FileHelper.DeleteFile(path);
                }
            }


            return Success(string.Format("总条数:{0},成功条数:{1},失败条数:{2}", result.TotalQuantity, result.FailureQuantity, result.ErrorMessage));
        }
    }
}