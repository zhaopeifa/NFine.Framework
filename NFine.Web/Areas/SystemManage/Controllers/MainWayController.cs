using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Code.Web;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    public class MainWayController : ControllerBase
    {
        private ProfileMainWayApp mainWayApp = new ProfileMainWayApp();
        private ProfileStreetApp StreetApp = new ProfileStreetApp();
        private UserApp userApp = new UserApp();

        [HttpGet]
        public ActionResult Import()
        {
            return View();
        }

        /// <summary>
        /// 数据导入
        /// </summary>
        /// <param name="CityId"></param>
        /// <param name="CountyId"></param>
        /// <param name="ProjectId"></param>
        /// <param name="isRename"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SummitImport(string CityId, string CountyId, int isRename = 1)
        {
            var file = Request.Files[0];

            string path = @"D:\项目\卫星设备台账流程管理\NFine.Web\bin\TemporaryFilesDiskPath\";
            string fileName = file.FileName;

            ImportResultModel result = new ImportResultModel();
            int failureQuantity, successfulQuantity;

            try
            {
                file.SaveAs(Path.Combine(path, fileName));

                #region 导出

                if (FileHelper.IsExistFile(Path.Combine(path, fileName)))
                {
                    FileHelper.DeleteFile(Path.Combine(path, fileName));

                    using (ExcelHelper exHelp = new ExcelHelper(Path.Combine(path, fileName)))
                    {
                        var datatable = exHelp.ExcelToDataTable(fileName, true);
                        ProfileMainWayEntity[] models = new ProfileMainWayEntity[datatable.Rows.Count];
                        ProfileMainWayEntity model;

                        for (int i = 0; i < datatable.Rows.Count; i++)
                        {
                            var mainWayName = datatable.Rows[i]["主路名"].ToString();
                            var jdName = datatable.Rows[i]["街道"].ToString();
                            var fCode = datatable.Rows[i]["序号"].ToString();

                            var StreetNamekey = StreetApp.GetDictionary(d => d.StreetName == jdName)[0].Key;

                            var streetModel = StreetApp.GetForm(StreetNamekey);

                            model = new ProfileMainWayEntity()
                            {
                                CityId = streetModel.CityId,
                                CountyId = streetModel.CountyId,
                                StreetId = streetModel.F_Id,
                                MainWayName = mainWayName,
                                F_EnCode = fCode
                            };

                            models[i] = model;
                        }
                        mainWayApp.ImportData(models, out successfulQuantity, out failureQuantity);
                        result.IsSucceed = true;
                        result.FailureQuantity = failureQuantity;
                        result.SuccessfulQuantity = successfulQuantity;
                        result.TotalQuantity = models.Length;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.ErrorMessage = ex.ToString();
            }
            finally
            {

            }
            return Success(string.Format("总条数:{0},成功条数:{1},失败条数:{2}", result.TotalQuantity, result.FailureQuantity, result.ErrorMessage));
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, string countyId)
        {
            var data = new
            {
                rows = mainWayApp.GetList(pagination, keyword, countyId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableGridJson()
        {
            string sql = "SELECT * FROM dbo.ProfileSanitationMainWay ";
            var data = mainWayApp.FildSql(sql);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取主路数据根据街道Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableGridJsonByStreetId(string StreetId)
        {
            var data = mainWayApp.GetDictionary(d => d.StreetId == StreetId);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = mainWayApp.GetForm(keyValue);
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
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProfileMainWayEntity mainWayEntity, string keyValue)
        {
            mainWayApp.SubmitForm(mainWayEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            mainWayApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}