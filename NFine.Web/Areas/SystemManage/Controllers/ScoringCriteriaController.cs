using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 评分标准
    /// </summary>
    public class ScoringCriteriaController : ControllerBase
    {
        public ActionResult GetTreeEnableJson()
        {

            var treeList = new List<TreeViewModel>();

            TreeViewModel tree = null;
            TreeViewModel treeGEntry = null;
            foreach (NFine.Domain.Enums.ProfileProjectTypeEnum myCode in Enum.GetValues(typeof(NFine.Domain.Enums.ProfileProjectTypeEnum)))
            {
                tree = new TreeViewModel();

                string strName = myCode.GetDescribe();//获取名称

                tree.id = "M" + ((int)myCode).ToString();
                tree.text = strName;
                tree.value = myCode.ToString();
                tree.parentId = "0";
                tree.isexpand = false;
                tree.complete = true;
                tree.hasChildren = true;

                switch (myCode)
                {
                    case ProfileProjectTypeEnum.Sanitation:
                        foreach (var item in ProfileGradeEnumExtension.SanitationGrade)
                        {
                            treeGEntry = new TreeViewModel();

                            treeGEntry.id = ((int)item).ToString();
                            treeGEntry.text = item.ToString();
                            treeGEntry.value = "2";
                            treeGEntry.parentId = "M" + ((int)myCode).ToString();
                            treeGEntry.isexpand = true;
                            treeGEntry.complete = true;
                            treeGEntry.hasChildren = false;

                            treeList.Add(treeGEntry);
                        }
                        break;
                    case ProfileProjectTypeEnum.Amenities:
                        break;
                    case ProfileProjectTypeEnum.FiveChaos:
                        break;
                    default:
                        break;
                }

                treeList.Add(tree);
            }
            //遍历二级



            return Content(treeList.TreeViewJson());
        }


    }
}