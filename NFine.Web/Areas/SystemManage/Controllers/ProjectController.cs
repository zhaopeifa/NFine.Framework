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
    public class ProjectController : ControllerBase
    {
        private ProfileProjectApp ProductApp = new ProfileProjectApp();
        private ProfileCityApp CityApp = new ProfileCityApp();
        private ProfileCountyApp CountyApp = new ProfileCountyApp();

        public ActionResult GetTreeEnableJson()
        {
            var cityData = CityApp.GetList();
            var countyData = CountyApp.GetList();
            var projectData = ProductApp.GetList();

            var treeList = new List<TreeViewModel>();

            TreeViewModel tree = null;
            //初始化一级 城市节点
            foreach (var item in cityData)
            {
                tree = new TreeViewModel();

                tree.id = item.F_Id;
                tree.text = item.CityName;
                tree.value = "1";
                tree.parentId = "0";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = true;
                treeList.Add(tree);
            }
            //初始化二级菜单 区县节点
            foreach (var item in countyData)
            {
                tree = new TreeViewModel();

                tree.id = item.F_Id;
                tree.text = item.CountyName;
                tree.value = "2";
                tree.parentId = item.CityId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = true;
                treeList.Add(tree);
            }

            foreach (var item in projectData)
            {
                tree = new TreeViewModel();

                tree.id = item.F_Id;
                tree.text = item.ProjectName;
                tree.value = "3";
                tree.parentId = item.CountyId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = true;


                switch (item.ProjectType)
                {
                    case "Sanitation": //环卫
                        TreeViewModel treeSani;
                        foreach (var entry in SanitationProjctEntry.SanitationProjctEntryCollecion)
                        {
                            treeSani = new TreeViewModel();
                            treeSani.id = entry.Id + item.F_Id;
                            treeSani.text = entry.Text;
                            treeSani.value = "4.1";
                            treeSani.parentId = entry.ParentNode == null ? item.F_Id : entry.ParentNode.Id + item.F_Id;
                            treeSani.isexpand = true;
                            treeSani.complete = true;
                            treeSani.hasChildren = entry.IsHaveChild;

                            treeList.Add(treeSani);
                        }
                        break;
                    case "Amenities"://市容
                        TreeViewModel treeAmen;
                        foreach (int myCode in Enum.GetValues(typeof(NFine.Domain.Enums.ProfileAmenitiesEnum)))
                        {
                            treeAmen = new TreeViewModel();

                            string strName = Enum.GetName(typeof(NFine.Domain.Enums.ProfileAmenitiesEnum), myCode);//获取名称

                            treeAmen.id = myCode.ToString(); ;
                            treeAmen.text = strName;
                            treeAmen.value = "4.2";
                            treeAmen.parentId = item.F_Id;
                            treeAmen.isexpand = true;
                            treeAmen.complete = true;
                            treeAmen.hasChildren = false;

                            treeList.Add(treeAmen);
                        }
                        break;
                    case "FiveChaos": //五乱

                        TreeViewModel treeFive;
                        foreach (int myCode in Enum.GetValues(typeof(NFine.Domain.Enums.ProfileFiveChaosEnum)))
                        {
                            treeFive = new TreeViewModel();

                            string strName = Enum.GetName(typeof(NFine.Domain.Enums.ProfileFiveChaosEnum), myCode);//获取名称

                            treeFive.id = myCode.ToString(); ;
                            treeFive.text = strName;
                            treeFive.value = "4.3";
                            treeFive.parentId = item.F_Id;
                            treeFive.isexpand = true;
                            treeFive.complete = false;
                            treeFive.hasChildren = false;

                            treeList.Add(treeFive);
                        }
                        break;
                    default:
                        break;
                }

                treeList.Add(tree);
            }

            //初始化区县下面的项目

            return Content(treeList.TreeViewJson());
        }
    }
}