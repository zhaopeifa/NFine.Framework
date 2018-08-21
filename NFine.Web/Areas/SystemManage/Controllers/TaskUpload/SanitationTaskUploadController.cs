using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Contracts;
using NFine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 环卫-任务上传
    /// </summary>
    public class SanitationTaskUploadController : ControllerBase
    {

        private ProfileTaskApp taskApp = new ProfileTaskApp();
        /// <summary>
        /// 评分标准
        /// </summary>
        private ProfileScoreCriteriaApp scApp = new ProfileScoreCriteriaApp();
        private ProfileDeducInsApp diApp = new ProfileDeducInsApp();


        [HttpGet]
        public ActionResult TaskDetail()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DeductForm()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = taskApp.GetNeedUpLoadTask(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        public ActionResult UpLoadTaskEntrFormy()
        {
            return View();
        }

        /// <summary>
        /// 加载评分标准
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetScireCriteria(string taskEntryId)
        {

            var data = taskApp.GetScireCriteria(taskEntryId);


            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetScireCriteriaTreeGridJson(string taskEntryId)
        {
            var data = taskApp.GetScireCriteria(taskEntryId);
            
            var treeList = new List<TreeGridModel>();

            List<ScorCriteriaClassifyTreeGridContracts> tests = new List<ScorCriteriaClassifyTreeGridContracts>();
            ScorCriteriaClassifyTreeGridContracts classifyGC = null;
            ScorCriteriaClassifyTreeGridContracts normGC = null;
            foreach (var item in data)
            {
                classifyGC = new ScorCriteriaClassifyTreeGridContracts();
                classifyGC.F_ParentId="0";
                classifyGC.Type = 1;
                classifyGC.F_Id = item.SClassifyId;
                classifyGC.SClassifyName = item.SClassifyName;
                classifyGC.SClassifyScore = item.SClassifyScore;


                foreach (var itemNorm in item.SNromCollecion)
                {
                    normGC = new ScorCriteriaClassifyTreeGridContracts();

                    normGC.Type=2;
                    normGC.F_Id = itemNorm.SNormId;
                    normGC.F_ParentId = item.SClassifyId;
                    normGC.SNormProjectName = itemNorm.SNormProjectName;
                    normGC.SNormStandardName = itemNorm.SNormStandardName;
                    normGC.SNormCondition = (int)itemNorm.SNormCondition;
                    tests.Add(normGC);
                }

                tests.Add(classifyGC);
            }

            foreach (ScorCriteriaClassifyTreeGridContracts item in tests)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = tests.Count(d => d.F_ParentId == item.F_Id) == 0 ? false : true;
                treeModel.id = item.F_Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.F_ParentId;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }

            return Content(treeList.TreeGridJson());
        }

        /// <summary>
        /// 上传扣分
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitForm(ProfileDeducInsSubMitContracts entity,string keyValue)
        {


            diApp.SubmitForm(entity, keyValue);
            return Success("操作成功!");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string normId,string taskEntryId)
        {
            var data = diApp.GetForm(normId, taskEntryId);
            return Content(data.ToJson());
        }

        public ActionResult UpLoadProcess(string id, string name, string type, string lastModifiedDate, int size, HttpPostedFileBase file)
        {
            string filePathName = string.Empty;

            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Upload");
            if (Request.Files.Count == 0)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" });
            }

            string ex = Path.GetExtension(file.FileName);
            filePathName = Guid.NewGuid().ToString("N") + ex;
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }
            file.SaveAs(Path.Combine(localPath, filePathName));

            return Json(new
            {
                jsonrpc = "2.0",
                id = id,
                filePath = "Upload" + "/" + filePathName
            });

        }
    }

    
}