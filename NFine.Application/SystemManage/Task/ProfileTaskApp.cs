using NFine.Code;
using NFine.Data.Extensions;
using NFine.Domain.Contracts;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.Enums;
using NFine.Repository.SystemManage;
using NFine.Web.Function;
using System;
using System.Collections.Generic;
using System.Data;
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
        /// 获取数据列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<ProfileTaskContracts> GetContractsList(Pagination pagination, string keyword, int? taskStateTypeInt = null)
        {
            var taskQuery = NFine.Data.Extensions.LinqSQLExtensions.IQueryable<ProfileTaskEntity>();

            if (!string.IsNullOrEmpty(keyword))
            {
                taskQuery = taskQuery.Where(t => t.F_EnCode.Contains(keyword));
            }

            if (taskStateTypeInt != null)
            {
                taskQuery = taskQuery.Where(t => t.State == taskStateTypeInt);
            }

            taskQuery = taskQuery.OrderByDescending(d => d.F_LastModifyTime).Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows);

            var contractsQuery = from taskEntityQ in taskQuery
                                 join userEntityQ in NFine.Data.Extensions.LinqSQLExtensions.IQueryable<UserEntity>()
                                 on taskEntityQ.PersonInChargeId equals userEntityQ.F_Id
                                 select new ProfileTaskContracts
                                 {
                                     F_Id = taskEntityQ.F_Id,
                                     State = taskEntityQ.State,
                                     F_EnCode = taskEntityQ.F_EnCode,
                                     CityId = taskEntityQ.CityId,
                                     CountyId = taskEntityQ.CountyId,
                                     ProjectType = taskEntityQ.ProjectType,
                                     CompanyId = taskEntityQ.CompanyId,
                                     StreetId = taskEntityQ.StreetId,
                                     PersonInChargeId = taskEntityQ.PersonInChargeId,
                                     PersonInChargeRealName = userEntityQ.F_RealName,
                                     DeliveryTime = taskEntityQ.DeliveryTime,
                                     CompletionTime = taskEntityQ.CompletionTime
                                 };

            return contractsQuery.ToList();
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
        /// 任务单作废
        /// </summary>
        public void TaskInvalid(string keyValue)
        {
            service.TaskInvalid(keyValue);

            try
            {
                //添加日志
                LogMess.addLog(DbLogType.Delete.ToString(), "作废成功", "作废任务单信息【" + this.GetForm(keyValue).F_EnCode + "】成功！");
            }
            catch { }
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
            int wastebasketInt = ProfileTaskEntryTypeEnum.Wastebasket.GetIntValue();
            int streetTrashInt = ProfileTaskEntryTypeEnum.StreetTrash.GetIntValue();

            int machineCleanCarInt = ProfileTaskEntryTypeEnum.MachineCleanCar.GetIntValue();
            int washTheCarInt = ProfileTaskEntryTypeEnum.WashTheCar.GetIntValue();
            int garbageTruckCarInt = ProfileTaskEntryTypeEnum.GarbageTruckCar.GetIntValue();
            int flyingCarInt = ProfileTaskEntryTypeEnum.FlyingCar.GetIntValue();
            int eightLadleCarInt = ProfileTaskEntryTypeEnum.EightLadleCar.GetIntValue();



            service.Command<ProfileTaskEntryEntity>((query) =>
            {
                //道路的
                var taskWayEntnty = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(wayTypeInt)).FirstOrDefault();
                result.WayId = taskWayEntnty.EntryDataId;
                result.WayPlaceCount = taskWayEntnty.BYMESS1 == null ? 0 : (int)taskWayEntnty.BYMESS1;


                result.TandasCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(tandaTypeInt)).Count();
                result.GarbageBoxCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(garbageBoxTypeInt)).Count();
                result.CompressionCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(compressionStationTypeInt)).Count();
                result.GreeningCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(greeningTypeInt)).Count();
                result.GreenResidentialCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(greenResidentialTypeInt)).Count();
                result.CesspoolCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(cesspoolTypeInt)).Count();
                result.WastebasketCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(wastebasketInt)).Count();
                result.StreetTrashCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(streetTrashInt)).Count();

                result.MachineCleanCarCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(machineCleanCarInt)).Count();
                result.WashTheCarCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(washTheCarInt)).Count();
                result.GarbageTruckCarCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(garbageTruckCarInt)).Count();
                result.FlyingCarCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(flyingCarInt)).Count();
                result.EightLadleCarCount = query.Where(d => d.TaskId == taskEntity.F_Id && d.TaskEntryType.Equals(eightLadleCarInt)).Count();
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

            //--------非定点------------



            return result;
        }

        public ITaskDetail[] GetTaskDetail(Pagination pagination, string taskId, ProfileTaskEntryTypeEnum taskEntryType)
        {
            ITaskDetail[] result = null;

            int taskEntryTypeInt = taskEntryType.GetIntValue();

            var taskQuery = from taskQ in LinqSQLExtensions.IQueryable<ProfileTaskEntity>()
                            join taskEntryQ in LinqSQLExtensions.IQueryable<ProfileTaskEntryEntity>()
                            on taskQ.F_Id equals taskEntryQ.TaskId
                            where taskQ.F_Id == taskId &&
                            taskEntryQ.TaskEntryType == taskEntryTypeInt
                            select new
                            {
                                F_Id = taskEntryQ.F_Id,
                                DataId = taskEntryQ.EntryDataId,
                                CompleteState = taskEntryQ.CompleteState,
                                PersonInChargeId = taskQ.PersonInChargeId,
                                CompletionTime = taskQ.CompletionTime,
                                OrdeNo = taskQ.F_EnCode,
                                DeliveryTime = taskQ.DeliveryTime
                            };

            //查一下用户名
            var userQuery = from taskQ in taskQuery
                            join userQ in LinqSQLExtensions.IQueryable<UserEntity>()
                            on taskQ.PersonInChargeId equals userQ.F_Id
                            select new
                            {
                                F_Id = taskQ.F_Id,
                                DataId = taskQ.DataId,
                                CompleteState = taskQ.CompleteState,
                                PersonInChargeId = taskQ.PersonInChargeId,
                                CompletionTime = taskQ.CompletionTime,
                                OrdeNo = taskQ.OrdeNo,
                                DeliveryTime = taskQ.DeliveryTime,
                                PersonInChargeName = userQ.F_RealName
                            };

            //根据不同的查询不同的数据

            switch (taskEntryType)
            {
                #region 道路
                case ProfileTaskEntryTypeEnum.Way://道路

                    var taskWayEntrysQuery = from dataQ in
                                                 (from taskQ in userQuery
                                                  join dataQ in LinqSQLExtensions.IQueryable<ProfileSanitationWayEntity>()
                                                  on taskQ.DataId equals dataQ.F_Id
                                                  select new
                                                  {
                                                      F_Id = taskQ.F_Id,
                                                      DataId = taskQ.DataId,
                                                      StreetId = dataQ.StreetId,
                                                      CompleteState = taskQ.CompleteState,
                                                      PersonInChargeId = taskQ.PersonInChargeId,
                                                      CompletionTime = taskQ.CompletionTime,
                                                      OrdeNo = taskQ.OrdeNo,
                                                      DeliveryTime = taskQ.DeliveryTime,
                                                      PersonInChargeName = taskQ.PersonInChargeName,
                                                      WayName = dataQ.WayName,
                                                      F_EnCode = dataQ.F_EnCode,
                                                      Origin = dataQ.Origin,
                                                      Destination = dataQ.Destination
                                                  })
                                             join streetQ in LinqSQLExtensions.IQueryable<ProfileStreetEntity>()
                                             on dataQ.StreetId equals streetQ.F_Id
                                             select new
                                             {
                                                 F_Id = dataQ.F_Id,
                                                 DataId = dataQ.DataId,
                                                 StreetId = dataQ.StreetId,
                                                 CompleteState = dataQ.CompleteState,
                                                 PersonInChargeId = dataQ.PersonInChargeId,
                                                 OrdeNo = dataQ.OrdeNo,
                                                 DeliveryTime = dataQ.DeliveryTime,
                                                 CompletionTime = dataQ.CompletionTime,
                                                 PersonInChargeName = dataQ.PersonInChargeName,
                                                 WayName = dataQ.WayName,
                                                 F_EnCode = dataQ.F_EnCode,
                                                 Origin = dataQ.Origin,
                                                 Destination = dataQ.Destination,
                                                 StreetIdName = streetQ.StreetName
                                             };




                    //设置总记录数
                    pagination.records = taskWayEntrysQuery.Count();
                    //设置分页数据
                    taskWayEntrysQuery = taskWayEntrysQuery.OrderBy(t => t.F_EnCode).Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows);

                    result = taskWayEntrysQuery.Select(t => new TaskDetailWayContracts()
                    {
                        F_Id = t.F_Id,
                        DataId = t.DataId,
                        CompleteState = t.CompleteState,
                        PersonInChargeId = t.PersonInChargeId,
                        CompletionTime = t.CompletionTime,
                        PersonInChargeName = t.PersonInChargeName,
                        WayName = t.WayName,
                        Origin = t.Origin,
                        Destination = t.Destination,
                        StreetId = t.StreetId,
                        StreetName = t.StreetIdName,
                        OrdeNo = t.OrdeNo,
                        DeliveryTime = t.DeliveryTime
                    }).ToArray();
                    break;
                #endregion

                #region 公厕

                case ProfileTaskEntryTypeEnum.Tandas://公厕

                    var taskTandasEntrysQuery = from dataQ in
                                                    (from taskQ in userQuery
                                                     join dataQ in LinqSQLExtensions.IQueryable<ProfileSanitationTandasEntity>()
                                                     on taskQ.DataId equals dataQ.F_Id
                                                     select new
                                                     {
                                                         F_Id = taskQ.F_Id,
                                                         DataId = taskQ.DataId,
                                                         CompleteState = taskQ.CompleteState,
                                                         PersonInChargeId = taskQ.PersonInChargeId,
                                                         CompletionTime = taskQ.CompletionTime,
                                                         OrdeNo = taskQ.OrdeNo,
                                                         DeliveryTime = taskQ.DeliveryTime,
                                                         PersonInChargeName = taskQ.PersonInChargeName,
                                                         StreetId = dataQ.StreetId,
                                                         Address = dataQ.Address,
                                                         CleaningUnit = dataQ.CleaningUnit
                                                     })
                                                join streetQ in LinqSQLExtensions.IQueryable<ProfileStreetEntity>()
                                                on dataQ.StreetId equals streetQ.F_Id
                                                select new
                                                {
                                                    F_Id = dataQ.F_Id,
                                                    DataId = dataQ.DataId,
                                                    CompleteState = dataQ.CompleteState,
                                                    PersonInChargeId = dataQ.PersonInChargeId,
                                                    CompletionTime = dataQ.CompletionTime,
                                                    OrdeNo = dataQ.OrdeNo,
                                                    DeliveryTime = dataQ.DeliveryTime,
                                                    PersonInChargeName = dataQ.PersonInChargeName,
                                                    StreetId = dataQ.StreetId,
                                                    Address = dataQ.Address,
                                                    CleaningUnit = dataQ.CleaningUnit,
                                                    StreetIdName = streetQ.StreetName
                                                };

                    //设置总记录数
                    pagination.records = taskTandasEntrysQuery.Count();
                    //设置分页数据
                    taskTandasEntrysQuery = taskTandasEntrysQuery.OrderBy(t => t.F_Id).Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows);

                    result = taskTandasEntrysQuery.Select(t => new TaskDetailTandasContracts()
                    {
                        F_Id = t.F_Id,
                        DataId = t.DataId,
                        CompleteState = t.CompleteState,
                        PersonInChargeId = t.PersonInChargeId,
                        CompletionTime = t.CompletionTime,
                        PersonInChargeName = t.PersonInChargeName,
                        Address = t.Address,
                        CleaningUnit = t.CleaningUnit,
                        StreetId = t.StreetId,
                        StreetName = t.StreetIdName,
                        OrdeNo = t.OrdeNo,
                        DeliveryTime = t.DeliveryTime
                    }).ToArray();

                    break;

                #endregion

                #region 垃圾箱房

                case ProfileTaskEntryTypeEnum.GarbageBox:

                    var taskGarbageBoxEntrysQuery = from dataQ in
                                                        (from taskQ in userQuery
                                                         join dataQ in LinqSQLExtensions.IQueryable<ProfileSanitationGarbageBoxEntity>()
                                                         on taskQ.DataId equals dataQ.F_Id
                                                         select new
                                                         {
                                                             F_Id = taskQ.F_Id,
                                                             DataId = taskQ.DataId,
                                                             CompleteState = taskQ.CompleteState,
                                                             PersonInChargeId = taskQ.PersonInChargeId,
                                                             CompletionTime = taskQ.CompletionTime,
                                                             OrdeNo = taskQ.OrdeNo,
                                                             DeliveryTime = taskQ.DeliveryTime,
                                                             PersonInChargeName = taskQ.PersonInChargeName,
                                                             Address = dataQ.Address,
                                                             StreetId = dataQ.StreetId,
                                                         })
                                                    join streetQ in LinqSQLExtensions.IQueryable<ProfileStreetEntity>()
                                                    on dataQ.StreetId equals streetQ.F_Id
                                                    select new
                                                    {
                                                        F_Id = dataQ.F_Id,
                                                        DataId = dataQ.DataId,
                                                        CompleteState = dataQ.CompleteState,
                                                        PersonInChargeId = dataQ.PersonInChargeId,
                                                        CompletionTime = dataQ.CompletionTime,
                                                        OrdeNo = dataQ.OrdeNo,
                                                        DeliveryTime = dataQ.DeliveryTime,
                                                        PersonInChargeName = dataQ.PersonInChargeName,
                                                        Address = dataQ.Address,
                                                        StreetId = dataQ.StreetId,
                                                        StreetName = streetQ.StreetName
                                                    };

                    //设置总记录数
                    pagination.records = taskGarbageBoxEntrysQuery.Count();
                    //设置分页
                    taskGarbageBoxEntrysQuery = taskGarbageBoxEntrysQuery.OrderBy(t => t.F_Id).Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows);

                    result = taskGarbageBoxEntrysQuery.Select(t => new TaskDetailGarbageBoxContracts()
                    {
                        F_Id = t.F_Id,
                        DataId = t.DataId,
                        CompleteState = t.CompleteState,
                        PersonInChargeId = t.PersonInChargeId,
                        CompletionTime = t.CompletionTime,
                        PersonInChargeName = t.PersonInChargeName,
                        Address = t.Address,
                        StreetId = t.StreetId,
                        StreetName = t.StreetName,
                        OrdeNo = t.OrdeNo,
                        DeliveryTime = t.DeliveryTime
                    }).ToArray();

                    break;


                #endregion

                #region 压缩站

                case ProfileTaskEntryTypeEnum.compressionStation:

                    var taskcompressionStationEntrysQuery = from dataQ in
                                                                (from taskQ in userQuery
                                                                 join dataQ in LinqSQLExtensions.IQueryable<ProfileSanitationCompressionStationEntity>()
                                                                 on taskQ.DataId equals dataQ.F_Id
                                                                 select new
                                                                 {
                                                                     F_Id = taskQ.F_Id,
                                                                     DataId = taskQ.DataId,
                                                                     CompleteState = taskQ.CompleteState,
                                                                     OrdeNo = taskQ.OrdeNo,
                                                                     DeliveryTime = taskQ.DeliveryTime,
                                                                     PersonInChargeId = taskQ.PersonInChargeId,
                                                                     CompletionTime = taskQ.CompletionTime,
                                                                     PersonInChargeName = taskQ.PersonInChargeName,
                                                                     Address = dataQ.Address,
                                                                     OpeningHours = dataQ.OpeningHours,
                                                                     StreetId = dataQ.StreetId
                                                                 })
                                                            join streetQ in LinqSQLExtensions.IQueryable<ProfileStreetEntity>()
                                                            on dataQ.StreetId equals streetQ.F_Id
                                                            select new
                                                            {
                                                                F_Id = dataQ.F_Id,
                                                                DataId = dataQ.DataId,
                                                                CompleteState = dataQ.CompleteState,
                                                                PersonInChargeId = dataQ.PersonInChargeId,
                                                                CompletionTime = dataQ.CompletionTime,
                                                                OrdeNo = dataQ.OrdeNo,
                                                                DeliveryTime = dataQ.DeliveryTime,
                                                                PersonInChargeName = dataQ.PersonInChargeName,
                                                                Address = dataQ.Address,
                                                                OpeningHours = dataQ.OpeningHours,
                                                                StreetId = dataQ.StreetId,
                                                                StreetName = streetQ.StreetName
                                                            };

                    //设置总记录数
                    pagination.records = taskcompressionStationEntrysQuery.Count();
                    //设置分页
                    taskcompressionStationEntrysQuery = taskcompressionStationEntrysQuery.OrderBy(t => t.F_Id).Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows);

                    result = taskcompressionStationEntrysQuery.Select(t => new TaskDetailCompressionStationContracts()
                    {
                        F_Id = t.F_Id,
                        DataId = t.DataId,
                        CompleteState = t.CompleteState,
                        PersonInChargeId = t.PersonInChargeId,
                        CompletionTime = t.CompletionTime,
                        PersonInChargeName = t.PersonInChargeName,
                        Address = t.Address,
                        OpeningHours = t.OpeningHours,
                        StreetId = t.StreetId,
                        StreetName = t.StreetName,
                        DeliveryTime = t.DeliveryTime,
                        OrdeNo = t.OrdeNo
                    }).ToArray();


                    break;

                #endregion

                #region 沿途绿化

                case ProfileTaskEntryTypeEnum.Greening:

                    var taskGreeningEntrysQuery = from dataQ in
                                                      (from taskQ in userQuery
                                                       join dataQ in LinqSQLExtensions.IQueryable<ProfileSanitationGreeningEntity>()
                                                       on taskQ.DataId equals dataQ.F_Id
                                                       select new
                                                       {
                                                           F_Id = taskQ.F_Id,
                                                           DataId = taskQ.DataId,
                                                           CompleteState = taskQ.CompleteState,
                                                           OrdeNo = taskQ.OrdeNo,
                                                           DeliveryTime = taskQ.DeliveryTime,
                                                           PersonInChargeId = taskQ.PersonInChargeId,
                                                           CompletionTime = taskQ.CompletionTime,
                                                           PersonInChargeName = taskQ.PersonInChargeName,
                                                           Address = dataQ.Address,
                                                           Origin = dataQ.Origin,
                                                           Destination = dataQ.Destination,
                                                           StreetId = dataQ.StreetId
                                                       })
                                                  join streetQ in LinqSQLExtensions.IQueryable<ProfileStreetEntity>()
                                                  on dataQ.StreetId equals streetQ.F_Id
                                                  select new
                                                  {
                                                      F_Id = dataQ.F_Id,
                                                      DataId = dataQ.DataId,
                                                      CompleteState = dataQ.CompleteState,
                                                      PersonInChargeId = dataQ.PersonInChargeId,
                                                      CompletionTime = dataQ.CompletionTime,
                                                      OrdeNo = dataQ.OrdeNo,
                                                      DeliveryTime = dataQ.DeliveryTime,
                                                      PersonInChargeName = dataQ.PersonInChargeName,
                                                      Address = dataQ.Address,
                                                      Origin = dataQ.Origin,
                                                      Destination = dataQ.Destination,
                                                      StreetId = dataQ.StreetId,
                                                      StreetName = streetQ.StreetName
                                                  };

                    //设置分页总数量
                    pagination.records = taskGreeningEntrysQuery.Count();
                    //设置分页
                    taskGreeningEntrysQuery = taskGreeningEntrysQuery.OrderBy(t => t.F_Id).Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows);

                    result = taskGreeningEntrysQuery.Select(t => new TaskDetailGreeningContracts()
                    {
                        F_Id = t.F_Id,
                        DataId = t.DataId,
                        CompleteState = t.CompleteState,
                        PersonInChargeId = t.PersonInChargeId,
                        CompletionTime = t.CompletionTime,
                        PersonInChargeName = t.PersonInChargeName,
                        Address = t.Address,
                        Origin = t.Origin,
                        Destination = t.Destination,
                        StreetId = t.StreetId,
                        StreetName = t.StreetName,
                        DeliveryTime = t.DeliveryTime,
                        OrdeNo = t.OrdeNo
                    }).ToArray();

                    break;

                #endregion

                #region 绿色账户小区

                case ProfileTaskEntryTypeEnum.GreenResidential:

                    var taskGreenResidentialEntrysQuery = from dataQ in
                                                              (from taskQ in userQuery
                                                               join dataQ in LinqSQLExtensions.IQueryable<ProfileSanitationGreenResidentialEntity>()
                                                               on taskQ.DataId equals dataQ.F_Id
                                                               select new
                                                               {
                                                                   F_Id = taskQ.F_Id,
                                                                   DataId = taskQ.DataId,
                                                                   CompleteState = taskQ.CompleteState,
                                                                   OrdeNo = taskQ.OrdeNo,
                                                                   DeliveryTime = taskQ.DeliveryTime,
                                                                   PersonInChargeId = taskQ.PersonInChargeId,
                                                                   CompletionTime = taskQ.CompletionTime,
                                                                   PersonInChargeName = taskQ.PersonInChargeName,
                                                                   Address = dataQ.Address,
                                                                   ResidentialName = dataQ.ResidentialName,
                                                                   StreetId = dataQ.StreetId
                                                               })
                                                          join streetQ in LinqSQLExtensions.IQueryable<ProfileStreetEntity>()
                                                          on dataQ.StreetId equals streetQ.F_Id
                                                          select new
                                                          {
                                                              F_Id = dataQ.F_Id,
                                                              DataId = dataQ.DataId,
                                                              CompleteState = dataQ.CompleteState,
                                                              PersonInChargeId = dataQ.PersonInChargeId,
                                                              CompletionTime = dataQ.CompletionTime,
                                                              OrdeNo = dataQ.OrdeNo,
                                                              DeliveryTime = dataQ.DeliveryTime,
                                                              PersonInChargeName = dataQ.PersonInChargeName,
                                                              Address = dataQ.Address,
                                                              ResidentialName = dataQ.ResidentialName,
                                                              StreetId = dataQ.StreetId,
                                                              StreetName = streetQ.StreetName
                                                          };

                    //设置分页总数量
                    pagination.records = taskGreenResidentialEntrysQuery.Count();
                    //设置分页
                    taskGreenResidentialEntrysQuery = taskGreenResidentialEntrysQuery.OrderBy(t => t.F_Id).Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows);

                    result = taskGreenResidentialEntrysQuery.Select(t => new TaskDetailGreenResidentialContracts()
                    {
                        F_Id = t.F_Id,
                        DataId = t.DataId,
                        CompleteState = t.CompleteState,
                        PersonInChargeId = t.PersonInChargeId,
                        CompletionTime = t.CompletionTime,
                        PersonInChargeName = t.PersonInChargeName,
                        Address = t.Address,
                        ResidentialName = t.ResidentialName,
                        StreetId = t.StreetId,
                        StreetName = t.StreetName,
                        DeliveryTime = t.DeliveryTime,
                        OrdeNo = t.OrdeNo
                    }).ToArray();

                    break;

                #endregion

                #region 倒粪池小便池

                case ProfileTaskEntryTypeEnum.cesspool:
                    var taskcesspoolEntrysQuery = from dataQ in
                                                      (from taskQ in userQuery
                                                       join dataQ in LinqSQLExtensions.IQueryable<ProfileSanitationCesspoolEntity>()
                                                       on taskQ.DataId equals dataQ.F_Id
                                                       select new
                                                       {
                                                           F_Id = taskQ.F_Id,
                                                           DataId = taskQ.DataId,
                                                           CompleteState = taskQ.CompleteState,
                                                           PersonInChargeId = taskQ.PersonInChargeId,
                                                           CompletionTime = taskQ.CompletionTime,
                                                           OrdeNo = taskQ.OrdeNo,
                                                           DeliveryTime = taskQ.DeliveryTime,
                                                           PersonInChargeName = taskQ.PersonInChargeName,
                                                           Address = dataQ.Address,
                                                           StreetId = dataQ.StreetId
                                                       })
                                                  join streetQ in LinqSQLExtensions.IQueryable<ProfileStreetEntity>()
                                                  on dataQ.StreetId equals streetQ.F_Id
                                                  select new
                                                  {
                                                      F_Id = dataQ.F_Id,
                                                      DataId = dataQ.DataId,
                                                      CompleteState = dataQ.CompleteState,
                                                      PersonInChargeId = dataQ.PersonInChargeId,
                                                      CompletionTime = dataQ.CompletionTime,
                                                      OrdeNo = dataQ.OrdeNo,
                                                      DeliveryTime = dataQ.DeliveryTime,
                                                      PersonInChargeName = dataQ.PersonInChargeName,
                                                      Address = dataQ.Address,
                                                      StreetId = dataQ.StreetId,
                                                      StreetName = streetQ.StreetName
                                                  };

                    //设置分页总数量
                    pagination.records = taskcesspoolEntrysQuery.Count();
                    //设置分页
                    taskcesspoolEntrysQuery = taskcesspoolEntrysQuery.OrderBy(t => t.F_Id).Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows);

                    result = taskcesspoolEntrysQuery.Select(t => new TaskDetailCesspoolContracts()
                    {
                        F_Id = t.F_Id,
                        DataId = t.DataId,
                        CompleteState = t.CompleteState,
                        PersonInChargeId = t.PersonInChargeId,
                        CompletionTime = t.CompletionTime,
                        PersonInChargeName = t.PersonInChargeName,
                        Address = t.Address,
                        StreetId = t.StreetId,
                        StreetName = t.StreetName,
                        DeliveryTime = t.DeliveryTime,
                        OrdeNo = t.OrdeNo
                    }).ToArray();

                    break;

                #endregion

                #region 废纸箱

                case ProfileTaskEntryTypeEnum.Wastebasket:

                    //设置分页总数量
                    pagination.records = userQuery.Count();
                    //设置分页
                    userQuery = userQuery.OrderBy(t => t.CompletionTime).Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows);

                    result = userQuery.Select(t => new TaskDetailWastebasketContracts()
                    {
                        F_Id = t.F_Id,
                        CompleteState = t.CompleteState,
                        CompletionTime = t.CompletionTime,
                        PersonInChargeId = t.PersonInChargeId,
                        PersonInChargeName = t.PersonInChargeName
                    }).ToArray();

                    break;

                #endregion
                case ProfileTaskEntryTypeEnum.StreetTrash:
                    break;
                case ProfileTaskEntryTypeEnum.MachineCleanCar:
                    break;
                case ProfileTaskEntryTypeEnum.WashTheCar:
                    break;
                case ProfileTaskEntryTypeEnum.GarbageTruckCar:
                    break;
                case ProfileTaskEntryTypeEnum.FlyingCar:
                    break;
                case ProfileTaskEntryTypeEnum.EightLadleCar:
                    break;
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<ProfileTaskContracts> GetNeedUpLoadTask(Pagination pagination, string keyword)
        {
            int toAuditInt = ProfileTaskStateEnum.ToAudit.GetIntValue();
            int backToInt = ProfileTaskStateEnum.BackTo.GetIntValue();


            var taskQyery = LinqSQLExtensions.IQueryable<ProfileTaskEntity>().Where(d => d.State == toAuditInt || d.State == backToInt);


            var taskQuery = NFine.Data.Extensions.LinqSQLExtensions.IQueryable<ProfileTaskEntity>();

            if (!string.IsNullOrEmpty(keyword))
            {
                taskQuery = taskQuery.Where(t => t.F_EnCode.Contains(keyword));
            }

            taskQuery = taskQuery.Where(t => t.State == toAuditInt || t.State == backToInt);

            taskQuery = taskQuery.OrderByDescending(d => d.F_LastModifyTime).Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows);

            var contractsQuery = from taskEntityQ in taskQuery
                                 join userEntityQ in NFine.Data.Extensions.LinqSQLExtensions.IQueryable<UserEntity>()
                                 on taskEntityQ.PersonInChargeId equals userEntityQ.F_Id
                                 select new ProfileTaskContracts
                                 {
                                     F_Id = taskEntityQ.F_Id,
                                     State = taskEntityQ.State,
                                     F_EnCode = taskEntityQ.F_EnCode,
                                     CityId = taskEntityQ.CityId,
                                     CountyId = taskEntityQ.CountyId,
                                     ProjectType = taskEntityQ.ProjectType,
                                     CompanyId = taskEntityQ.CompanyId,
                                     StreetId = taskEntityQ.StreetId,
                                     PersonInChargeId = taskEntityQ.PersonInChargeId,
                                     PersonInChargeRealName = userEntityQ.F_RealName,
                                     DeliveryTime = taskEntityQ.DeliveryTime,
                                     CompletionTime = taskEntityQ.CompletionTime
                                 };

            return contractsQuery.ToList();
        }


        /// <summary>
        /// 获取对应评分标准
        /// </summary>
        public List<ScireCriteriaContracts> GetScireCriteria(string taskEnryId)
        {
            List<ScireCriteriaContracts> result = new List<ScireCriteriaContracts>();

            //获取对应评分标准
            var query = LinqSQLExtensions.IQueryable<ProfileTaskEntryEntity>().Where(d => d.F_Id == taskEnryId);

            if (query.Count() <= 0)
            {
                throw new Exception("未找到对应任务明细!");
            }

            var taskEntry = query.FirstOrDefault();

            string scEntryName = "";
            string scTypeName = "";

            //寻找对应评分标准
            switch ((ProfileTaskEntryTypeEnum)taskEntry.TaskEntryType)
            {
                #region 道路评分标准
                case ProfileTaskEntryTypeEnum.Way:
                    scEntryName = "道路";

                    //获取对应道路去拿等级
                    var wayDataQuery = LinqSQLExtensions.IQueryable<ProfileSanitationWayEntity>().Where(d => d.F_Id == taskEntry.EntryDataId);
                    if (wayDataQuery.Count() > 0)
                    {
                        var wayData = wayDataQuery.FirstOrDefault();
                        switch ((ProfileWayGradeEnum)wayData.WayGrade)
                        {
                            case ProfileWayGradeEnum.一级道路:
                                scTypeName = "一级道路";
                                break;
                            case ProfileWayGradeEnum.二级道路:
                                scTypeName = "二级道路";
                                break;
                            case ProfileWayGradeEnum.三级及其它:
                                scTypeName = "三级道路";
                                break;
                            default:
                                scTypeName = "无";
                                break;
                        }
                    }

                    break;

                #endregion
                #region 公厕评分标准

                case ProfileTaskEntryTypeEnum.Tandas:
                    scEntryName = "公厕";

                    var tandasDataQuery = LinqSQLExtensions.IQueryable<ProfileSanitationTandasEntity>().Where(d => d.F_Id == taskEntry.EntryDataId);
                    if (tandasDataQuery.Count() > 0)
                    {
                        var tandasData = tandasDataQuery.FirstOrDefault();
                        switch ((ProfileTandasGradeEnum)tandasData.Grade)
                        {
                            case ProfileTandasGradeEnum.一类公厕:
                                scTypeName = "一类公厕";
                                break;
                            case ProfileTandasGradeEnum.二类公厕:
                                scTypeName = "二类公厕";
                                break;
                            case ProfileTandasGradeEnum.三类公厕:
                                scTypeName = "三类公厕";
                                break;
                            default:
                                break;
                        }
                    }

                    break;

                #endregion
                case ProfileTaskEntryTypeEnum.GarbageBox:
                    break;
                case ProfileTaskEntryTypeEnum.compressionStation:
                    break;
                case ProfileTaskEntryTypeEnum.Greening:
                    break;
                case ProfileTaskEntryTypeEnum.GreenResidential:
                    break;
                case ProfileTaskEntryTypeEnum.cesspool:
                    break;
                case ProfileTaskEntryTypeEnum.Wastebasket:
                    break;
                case ProfileTaskEntryTypeEnum.StreetTrash:
                    break;
                case ProfileTaskEntryTypeEnum.MachineCleanCar:
                    break;
                case ProfileTaskEntryTypeEnum.WashTheCar:
                    break;
                case ProfileTaskEntryTypeEnum.GarbageTruckCar:
                    break;
                case ProfileTaskEntryTypeEnum.FlyingCar:
                    break;
                case ProfileTaskEntryTypeEnum.EightLadleCar:
                    break;
                default:
                    break;
            }

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT c.Name AS SEntryName,c.SEntryId AS SEntryId,b.Name AS STypeName,b.STypeId AS STypeId,a.SClassifyId as SClassifyId,a.SClassifyName as SClassifyName,a.Score as Score  ");
            sqlStr.Append("FROM ProfileScoreCriteria_Classify a LEFT JOIN ProfileScoreCriteria_Type b ");
            sqlStr.Append("ON a.STypeId=b.STypeId LEFT JOIN ProfileScoreCriteria_Entry c ");
            sqlStr.Append("  ON b.SEntryId=c.SEntryId where 1=1 AND c.Name='" + scEntryName + "' AND b.Name='" + scTypeName + "'  ");

            DataTable table = DbHelper.ExecuteDataTable(sqlStr.ToString(), null);

            ScireCriteriaContracts scContr = null;
            ScireCriteriaNormContracts scNormContr = null;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                scContr = new ScireCriteriaContracts();

                if (table.Rows[i]["SEntryName"] != null)
                {
                    scContr.SEntryName = table.Rows[i]["SEntryName"].ToString();
                }
                if (table.Rows[i]["SEntryId"] != null)
                {
                    scContr.SEntryId = table.Rows[i]["SEntryId"].ToString();
                }
                if (table.Rows[i]["STypeName"] != null)
                {
                    scContr.STypeName = table.Rows[i]["STypeName"].ToString();
                }
                if (table.Rows[i]["STypeId"] != null)
                {
                    scContr.STypeId = table.Rows[i]["STypeId"].ToString();
                }
                if (table.Rows[i]["SClassifyId"] != null)
                {
                    scContr.SClassifyId = table.Rows[i]["SClassifyId"].ToString();
                }
                if (table.Rows[i]["SClassifyName"] != null)
                {
                    scContr.SClassifyName = table.Rows[i]["SClassifyName"].ToString();
                }
                if (table.Rows[i]["Score"] != null)
                {
                    scContr.SClassifyScore = (int)table.Rows[i]["Score"];
                }


                sqlStr.Clear();
                sqlStr.Append("SELECT SNormId,SNormProjectName,SNormStandardName,Condition FROM ProfileScireCriteria_Norm WHERE SClassifyId='" + scContr.SClassifyId + "'");

                var normTable = DbHelper.ExecuteDataTable(sqlStr.ToString(), null);

                scContr.SNromCollecion = new List<ScireCriteriaNormContracts>();


                for (int j = 0; j < normTable.Rows.Count; j++)
                {
                    scNormContr = new ScireCriteriaNormContracts();

                    if (normTable.Rows[j]["SNormId"] != null)
                    {
                        scNormContr.SNormId = normTable.Rows[j]["SNormId"].ToString();
                    }
                    if (normTable.Rows[j]["SNormProjectName"] != null)
                    {
                        scNormContr.SNormProjectName = normTable.Rows[j]["SNormProjectName"].ToString();
                    }
                    if (normTable.Rows[j]["SNormStandardName"] != null)
                    {
                        scNormContr.SNormStandardName = normTable.Rows[j]["SNormStandardName"].ToString();
                    }
                    if (normTable.Rows[j]["Condition"] != null)
                    {
                        scNormContr.SNormCondition = (int)normTable.Rows[j]["Condition"];
                    }

                    scContr.SNromCollecion.Add(scNormContr);
                }


                result.Add(scContr);
            }


            return result;
        }
    }
}
