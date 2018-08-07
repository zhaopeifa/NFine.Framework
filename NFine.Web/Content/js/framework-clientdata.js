var clients = [];
$(function () {
    clients = $.clientsInit();
})

$.clientsInit = function () {
    var dataJson = {
        dataItems: [],
        organize: [],
        role: [],
        duty: [],
        user: [],
        profileCity: [],
        profileCounty: [],
        profileStreet: [],
        authorizeMenu: [],
        authorizeButton: [],
        ProfileOperationsCompany: [],//环卫作业公司
        ProfileWayGrades: [],//道路等级
        ProfileTandasGrade: [],//厕所等级
        ProfileTandasManagementForms: [],//公厕管理形式,
        ProfileSanitationGarbageBoxType: [],//垃圾箱房类型
        ProfileWatersType: [],//水域类型
        ProfileGovernSort: [],//三年治理计划分类
        ProfileEnvironmentType: [],//三年治理计划环境类型
        ProfileGovernType:[]//三年治理计划类型
    };
    var init = function () {
        $.ajax({
            url: "/ClientsData/GetClientsDataJson",
            type: "get",
            dataType: "json",
            async: false,
            success: function (data) {
                dataJson.dataItems = data.dataItems;
                dataJson.organize = data.organize;
                dataJson.role = data.role;
                dataJson.duty = data.duty;
                dataJson.authorizeMenu = eval(data.authorizeMenu);
                dataJson.authorizeButton = data.authorizeButton;
                dataJson.profileCity = data.profileCity;
                dataJson.ProfileOperationsCompany = data.ProfileOperationsCompany;
                dataJson.profileCounty = data.profileCounty;
                dataJson.profileStreet = data.profileStreet;
                dataJson.ProfileWayGrades = data.ProfileWayGrades;
                dataJson.ProfileTandasGrade = data.ProfileTandasGrade;
                dataJson.ProfileTandasManagementForms = data.ProfileTandasManagementForms;
                dataJson.ProfileSanitationGarbageBoxType = data.ProfileSanitationGarbageBoxType;
                dataJson.ProfileWatersType = data.ProfileWatersType;
                dataJson.ProfileGovernSort = data.ProfileGovernSort;
                dataJson.ProfileEnvironmentType = data.ProfileEnvironmentType;
                dataJson.ProfileGovernType = data.ProfileGovernType;
            }
        });
    }
    init();
    return dataJson;
}