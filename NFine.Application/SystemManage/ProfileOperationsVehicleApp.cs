using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Repository.SystemManage;
using NFine.Web.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.SystemManage
{
    /// <summary>
    /// 环卫作业车辆
    /// </summary>
    public class ProfileOperationsVehicleTypeApp
    {
        private ProfileOperationsVehicleTypeRepository service = new ProfileOperationsVehicleTypeRepository();

        public List<ProfileOperationsVehicleTypeEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString());
        }

        public List<ProfileOperationsVehicleTypeEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ProfileOperationsVehicleTypeEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.VehicleTypeName.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }
        /// <summary>
        /// 只寻找一级菜单
        /// </summary>
        /// <returns></returns>
        public List<ProfileOperationsVehicleTypeEntity> GetEnableList()
        {
            string sql = "SELECT * FROM ProfileOperationsVehicleType WHERE VehicleTypeParentId IS NULL";
            return FildSql(sql);
          
        }

        //获取数据字典列表
        public List<ProfileOperationsVehicleTypeEntity> GetList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<ProfileOperationsVehicleTypeEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_Id == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.VehicleTypeName.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.F_CreatorTime).ToList();
        }

        public ProfileOperationsVehicleTypeEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void DeleteForm(string keyValue)
        {
            service.Delete(GetForm(keyValue));

            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除作业车辆信息【" + GetForm(keyValue).VehicleTypeName + "】成功！");
            }
            catch { }
        }

        public void SubmitForm(ProfileOperationsVehicleTypeEntity vehicleEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                vehicleEntity.Modify(keyValue);
                service.Update(vehicleEntity);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "修改作业车辆【" + vehicleEntity.VehicleTypeName + "】成功！");
                }
                catch { }
            }
            else
            {
                vehicleEntity.Create();
                service.Insert(vehicleEntity);
                try
                {
                    //添加日志
                    LogMess.addLog(DbLogType.Update.ToString(), "修改成功", "新建作业车辆【" + vehicleEntity.VehicleTypeName + "】成功！");
                }
                catch { }
            }
        }
    }
}
