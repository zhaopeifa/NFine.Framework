using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 评分标准详细
    /// </summary>
    public class ScireCriteria_NormController : ControllerBase
    {
        private ProfileScoreCriteriaApp App = new ProfileScoreCriteriaApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeEnableJson()
        {
            //获取大类数据 
            var entryData = App.GetEntryDictionary("SELECT * FROM ProfileScoreCriteria_Entry");

            var treeList = new List<TreeViewModel>();

            //设置大类tree
            TreeViewModel tree = null;
            foreach (var entryItem in entryData)
            {
                tree = new TreeViewModel();

                tree.id = entryItem.Key;
                tree.text = entryItem.Value;
                tree.value = "1";
                tree.parentId = "0";
                tree.isexpand = false;
                tree.complete = true;
                tree.hasChildren = true;

                treeList.Add(tree);
                

                //不要二级了

                //获取三级
                var classifyData = App.GetClassifyGroupDictionary(entryItem.Key);

                TreeViewModel classifyTree = null;
                foreach (var classifyItem in classifyData)
                {
                    classifyTree = new TreeViewModel();
                    classifyTree.id = classifyItem.Key;
                    classifyTree.text = classifyItem.Value;
                    classifyTree.value = "3";
                    classifyTree.parentId = entryItem.Key;
                    classifyTree.isexpand = false;
                    classifyTree.complete = true;
                    classifyTree.hasChildren = false;

                    treeList.Add(classifyTree);
                }


            }

            return Content(treeList.TreeViewJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, string groupId)
        {
            var data = new
            {
                rows = App.GetNormTable(pagination, keyword, groupId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };

            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ProfileScireCriteria_NormEntity Entity, string normGroupId, string classifyGroupId)
        {
            App.SubmitNormForm(Entity, normGroupId, classifyGroupId);
            return Success("操作成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string normGroupId)
        {
            var data = App.GetNormForm(normGroupId);

            return Content(data.ToJson());
        }


        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string groupId)
        {
            App.DeleteNormForm(groupId);
            return Success("删除成功。");
        }
    }
}