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
    public class SanitationWayController : ControllerBase
    {
        private ProfileSanitationWayApp wayApp = new ProfileSanitationWayApp();
        private UserApp userApp = new UserApp();
        private ProfileStreetApp StreetApp = new ProfileStreetApp();
        private ProfileMainWayApp MainWayApp = new ProfileMainWayApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, string cityId, string countyId, string projectId, string streetId)
        {
            var data = new
            {
                rows = wayApp.GetList(pagination, keyword, projectId, streetId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProfileSanitationWayEntity WayEntity, string keyValue)
        {
            SummitImport(WayEntity.CityId, WayEntity.CountyId, WayEntity.ProjectId);

            wayApp.SubmitForm(WayEntity, keyValue);
            return Success("操作成功。");
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
        public ActionResult SummitImport(string CityId, string CountyId, string projectId, int isRename = 1)
        {
            projectId = "dd1bbf6b-bcea-4cad-851b-1be4adf71860";

            string path = @"C:\Users\zps\Desktop\聚力环境测评系统样版\聚力环境测评系统样版\附件6、虹口市容点位\";
            string fileName = "Book1.xlsx";
            #region 导出

            if (FileHelper.IsExistFile(Path.Combine(path, fileName)))
            {


                using (ExcelHelper exHelp = new ExcelHelper(Path.Combine(path, fileName)))
                {
                    var datatable = exHelp.ExcelToDataTable(fileName, true);


                    //ProfileAmenitiesConstructionSiteEntiy

                    ProfileAmenitiesConstructionSiteEntity[] models = new ProfileAmenitiesConstructionSiteEntity[datatable.Rows.Count];
                    ProfileAmenitiesConstructionSiteEntity model;

                    for (int i = 0; i < datatable.Rows.Count; i++)
                    {
                        try
                        {
                            var jdName = datatable.Rows[i]["街道"].ToString();
                            var fCode = datatable.Rows[i]["序号"].ToString();
                            var address = datatable.Rows[i]["地址"].ToString();
                            var name = datatable.Rows[i]["工地名称"].ToString();



                            var StreetNamekey = StreetApp.GetDictionary(d => d.StreetName == jdName)[0].Key;

                            var streetModel = StreetApp.GetForm(StreetNamekey);

                            model = new ProfileAmenitiesConstructionSiteEntity()
                            {
                                CityId = streetModel.CityId,
                                CountyId = streetModel.CountyId,
                                StreetId = streetModel.F_Id,
                                Address = address,
                                F_EnCode = fCode,
                                SiteName = name,
                                ProjectId = projectId
                            };

                            models[i] = model;
                        }
                        catch
                        {

                        }
                    }
                    var thapp = new ProfileAmenitiesConstructionSiteApp();
                    foreach (var item in models)
                    {
                        try
                        {
                            if (item == null)
                                continue;
                            thapp.SubmitForm(item, string.Empty);
                        }
                        catch
                        {

                        }

                    }

                }
            }
            #endregion


            return Success("ss");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            wayApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 获取道路等级
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableWayGradeGridJson()
        {

            List<object> list = new List<object>();

            foreach (int myCode in Enum.GetValues(typeof(NFine.Domain.Enums.ProfileWayGradeEnum)))
            {
                string strName = Enum.GetName(typeof(NFine.Domain.Enums.ProfileWayGradeEnum), myCode);//获取名称

                list.Add(new { id = myCode, text = strName });
            }
            return Content(list.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = wayApp.GetForm(keyValue);
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


    }
}