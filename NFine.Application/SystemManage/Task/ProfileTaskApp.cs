using NFine.Code;
using NFine.Domain.Contracts;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.Enums;
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
    /// 环评-环卫-任务
    /// </summary>
    public class ProfileTaskApp
    {
        private ProfileTaskRepository service = new ProfileTaskRepository();

        /// <summary>
        /// 使用sql查询
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<ProfileTaskEntity> FildSql(string enCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(enCode);
            return service.FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pagination">分页，排序参数</param>
        /// <param name="keyword">检索关键字</param>
        /// <returns></returns>
        public List<ProfileTaskEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ProfileTaskEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_EnCode.Contains(keyword));
            }

            return service.FindList(expression, pagination);
        }

        /// <summary>
        /// 根据id获取单挑数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ProfileTaskEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void SubmitForm(TaskInsertContracts taskEntity, string keyValue)
        {
            service.SubmitForm(taskEntity, keyValue);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            var deleteEntuty = GetForm(keyValue);
            deleteEntuty.Remove();

            this.service.DeleteForm(deleteEntuty);
            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Delete.ToString(), "删除成功", "删除任务单信息【" + deleteEntuty.F_DeleteUserId + "】成功！");
            }
            catch { }
        }

        /// <summary>
        /// 任务派发
        /// </summary>
        public void TaskDistributed(string keyValue)
        {
            service.TaskDistributed(keyValue);
        }

        /// <summary>
        /// 获取任务单
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public TaskInsertContracts GetTaskInsertContractsForm(string keyValue)
        {

            TaskInsertContracts result = new TaskInsertContracts();

            var taskEntity = GetForm(keyValue);

            result.F_Id = taskEntity.F_Id;
            result.CityId = taskEntity.CityId;
            result.CountyId = taskEntity.CountyId;
            result.StreetId = taskEntity.StreetId;
            result.CompanyId = taskEntity.CompanyId;
            result.CompletionTime = taskEntity.CompletionTime;
            result.PersonInChargeId = taskEntity.PersonInChargeId;

            int wayTypeInt = ProfileTaskEntryTypeEnum.Way.GetIntValue();
            int tandaTypeInt = ProfileTaskEntryTypeEnum.Tandas.GetIntValue();
            int garbageBoxTypeInt = ProfileTaskEntryTypeEnum.GarbageBox.GetIntValue();
            int compressionStationTypeInt = ProfileTaskEntryTypeEnum.compressionStation.GetIntValue();
            int greeningTypeInt = ProfileTaskEntryTypeEnum.Greening.GetIntValue();
            int greenResidentialTypeInt = ProfileTaskEntryTypeEnum.GreenResidential.GetIntValue();
            int cesspoolTypeInt = ProfileTaskEntryTypeEnum.cesspool.GetIntValue();

            service.Command<ProfileTaskEntryEntity>((query) =>
            {
                result.WayId = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(wayTypeInt)).Select(d => d.EntryDataId).FirstOrDefault();
                result.TandasCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(tandaTypeInt)).Count();
                result.GarbageBoxCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(garbageBoxTypeInt)).Count();
                result.CompressionCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(compressionStationTypeInt)).Count();
                result.GreeningCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(greeningTypeInt)).Count();
                result.GreenResidentialCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(greenResidentialTypeInt)).Count();
                result.CesspoolCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(cesspoolTypeInt)).Count();
            });

            //获取主路
            service.Command<ProfileSanitationWayEntity>((query) =>
            {
                result.MainWayId = query.Where(d => d.F_Id == result.WayId).Select(d => d.MainWayId).FirstOrDefault();
            });

            return result;
        }

        /// <summary>
        /// 任务分配获取街道下检查项各个的数量
        /// </summary>
        /// <param name="streetId"></param>
        /// <returns></returns>
        public TaskScreeningCheckPostCountContracts GetCheckPostCount(string streetId)
        {
            TaskScreeningCheckPostCountContracts result = new TaskScreeningCheckPostCountContracts();

            #region 获取街道下公厕数量

            service.Command<ProfileSanitationTandasEntity>((query) =>
            {
                result.TandasCount = query.Where(d => d.StreetId == streetId).Count();
            });

            #endregion

            #region 获取街道下倒粪池小便池数量
            result.CesspoolCount = Data.Extensions.LinqSQLExtensions.IQueryable<ProfileSanitationCesspoolEntity>().Where(d => d.StreetId == streetId).Count();

            #endregion

            #region 获取街道下垃圾箱房数量
            service.Command<ProfileSanitationGarbageBoxEntity>((query) =>
            {
                result.GarbageBox = query.Where(d => d.StreetId == streetId).Count();
            });
            #endregion

            #region 获取街道下压缩站数量

            service.Command<ProfileSanitationCompressionStationEntity>((query) =>
            {
                result.compressionStation = query.Where(d => d.StreetId == streetId).Count();
            });

            #endregion

            #region 获取街道下沿途绿化数量

            service.Command<ProfileSanitationGreeningEntity>((query) =>
            {
                result.Greening = query.Where(d => d.StreetId == streetId).Count();
            });

            #endregion

            #region 获取街道下绿色账户小区数量
            service.Command<ProfileSanitationGreenResidentialEntity>((query) =>
            {
                result.GreenResidential = query.Where(d => d.StreetId == streetId).Count();
            });
            #endregion

            return result;
        }
    }
}
