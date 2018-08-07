using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Contracts;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 环卫评分标准
    /// </summary>
    public class ScoringCriteriaSanitationController : ControllerBase
    {
        private ScoringCriteriaApp App = new ScoringCriteriaApp();
        private UserApp userApp = new UserApp();


        [HttpGet]
        public ActionResult FormOptions()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, int GradeType)
        {
            var data = new
            {
                rows = App.GetList(pagination, keyword, (Domain.Enums.ProfileGradeBasicDataEnum)GradeType),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetNormGridJson(Pagination pagination, string optionId, string keyword)
        {
            var data = new
            {
                rows = App.GetNormList(pagination, optionId, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取当前关联
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetEnableGridJson(int GradeType)
        {

            var gradeTypes = ProfileGradeEnumExtension.GetGradeType((ProfileGradeBasicDataEnum)GradeType);

            List<object> list = new List<object>();

            foreach (int myCode in gradeTypes)
            {
                string strName = Enum.GetName(typeof(NFine.Domain.Enums.ProfileScoringClassifyEntryType), myCode);//获取名称

                list.Add(new { key = myCode, value = strName });
            }
            return Content(list.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult Getrelevance(string keyValue)
        {
            string sql = "SELECT top 1 * FROM ProfileGrading_Type_Rlation WHERE ProfileGradingTypeId='" + keyValue + "'";
            var typeRelations = App.TypeRlationFildSql(sql);

            if (typeRelations.Count > 0)
            {
                string[] ids = typeRelations[0].ProfileGradeType.Split(',');
                return Content(ids.Select(d => int.Parse(d)).ToArray().ToJson());
            }
            return null;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetOptions(string keyValue)
        {
            string sql = "SELECT * FROM ProfileGrading_Options WHERE ProfileGradingTypeId='" + keyValue + "'";
            var typeRelations = App.OptionsFindSql(sql).Select(d => new ProfileGrading_OptionsContracts() { id = d.F_Id, name = d.Name });
            return Content(typeRelations.ToJson()); ;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetOptionsTreeEnableJson(string keyValue)
        {
            string sql = "SELECT * FROM ProfileGrading_Options WHERE ProfileGradingTypeId='" + keyValue + "'";
            var typeRelations = App.OptionsFindSql(sql);
            var treeList = new List<TreeViewModel>();

            TreeViewModel tree = null;
            //初始化一级 城市节点
            foreach (var item in typeRelations)
            {
                tree = new TreeViewModel();

                tree.id = item.F_Id;
                tree.text = item.Name;
                tree.value = item.F_Id;
                tree.parentId = "0";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                treeList.Add(tree);
            }

            return Content(treeList.TreeViewJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProfileGrading_TypeEntity Entity, int GradeType, string keyValue, string Relevance, string Options)
        {
            var options = Options.ToObject<ProfileGrading_OptionsContracts[]>();

            App.SubmitForm(Entity, GradeType, keyValue, Relevance, options);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitNormForm(ProfileGrading_NormEntity Entity, string keyValue)
        {
            App.SubmitNormForm(Entity, keyValue);

            return Success("操作成功。");
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

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetNormForm(string keyValue)
        {
            var data = App.GetNormForm(keyValue);
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
            App.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNormForm(string keyValue)
        {
            App.DeleteNormForm(keyValue);
            return Success("删除成功。");
        }
    }
}