using NFine.Data;
using NFine.Domain.Contracts;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using NFine.Code;


namespace NFine.Repository.SystemManage
{
    /// <summary>
    /// 环评-环卫-任务
    /// </summary>
    public class ProfileTaskRepository : RepositoryBase<ProfileTaskEntity>
    {
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(TaskInsertContracts taskContracts, string keyValue)
        {

            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))//修改
                {
                    var taskEntity = db.FindEntity<ProfileTaskEntity>(keyValue);

                    taskEntity.ProjectType = 1;
                    taskEntity.CityId = taskContracts.CityId;
                    taskEntity.CompanyId = taskContracts.CompanyId;
                    taskEntity.CountyId = taskContracts.CountyId;
                    taskEntity.StreetId = taskContracts.StreetId;
                    taskEntity.State = ProfileTaskStateEnum.NotToSend.GetIntValue();
                    taskEntity.PersonInChargeId = taskContracts.PersonInChargeId;
                    taskEntity.CompletionTime = taskContracts.CompletionTime;

                    taskEntity.Modify(keyValue);

                    db.Update<ProfileTaskEntity>(taskEntity);


                    //删除之前的字表数据
                    var taskEntrys = db.IQueryable<ProfileTaskEntryEntity>().Where(d => d.TaskId == taskEntity.F_Id).ToArray();
                    foreach (var item in taskEntrys)
                    {
                        db.Delete<ProfileTaskEntryEntity>(item);
                    }

                    #region 道路
                    var wayTaskEntry = new ProfileTaskEntryEntity()
                    {
                        CityId = taskEntity.CityId,
                        CompanyId = taskEntity.CompanyId,
                        CountyId = taskEntity.CountyId,
                        TaskId = taskEntity.F_Id,
                        ProjectType = 1,
                        TaskEntryType = ProfileTaskEntryTypeEnum.Way.GetIntValue(),
                        EntryDataId = taskContracts.WayId,
                        StreetId = taskEntity.StreetId,
                        F_EnCode = taskEntity.F_EnCode,
                        PersonInChargeId = taskEntity.PersonInChargeId,
                        BYMESS1 = taskContracts.WayPlaceCount,
                        IsFixedPoint = true,
                        CompleteState = false
                    };

                    wayTaskEntry.Create();

                    db.Insert<ProfileTaskEntryEntity>(wayTaskEntry);

                    #endregion

                    #region 环卫公厕


                    string[] thandasTaskIds = db.IQueryable<ProfileSanitationTandasEntity>().Where(d => d.StreetId == taskEntity.StreetId).OrderBy(d => Guid.NewGuid()).Take(taskContracts.TandasCount).Select(d => d.F_Id).ToArray();

                    ProfileTaskEntryEntity thandasTaskEntry;
                    for (int i = 0; i < thandasTaskIds.Length; i++)
                    {
                        thandasTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.Tandas.GetIntValue(),
                            EntryDataId = thandasTaskIds[i],
                            StreetId = taskEntity.StreetId,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = true,
                            CompleteState = false
                        };
                        thandasTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(thandasTaskEntry);
                    }

                    #endregion

                    #region 沿途垃圾收集设备 倒粪池 小便池

                    string[] cesspoolIds = db.IQueryable<ProfileSanitationCesspoolEntity>().Where(d => d.StreetId == taskContracts.StreetId).OrderBy(d => Guid.NewGuid()).Take(taskContracts.CesspoolCount).Select(d => d.F_Id).ToArray();

                    ProfileTaskEntryEntity cesspoolTaskEntry;
                    for (int i = 0; i < cesspoolIds.Length; i++)
                    {
                        cesspoolTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.cesspool.GetIntValue(),
                            EntryDataId = cesspoolIds[i],
                            StreetId = taskEntity.StreetId,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = true,
                            CompleteState = false
                        };

                        cesspoolTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(cesspoolTaskEntry);
                    }
                    #endregion

                    #region 垃圾箱房
                    string[] garbageBoxTaskIds = db.IQueryable<ProfileSanitationGarbageBoxEntity>().Where(d => d.StreetId == taskContracts.StreetId).OrderBy(d => Guid.NewGuid()).Take(taskContracts.GarbageBoxCount).Select(d => d.F_Id).ToArray();

                    ProfileTaskEntryEntity garbageBoxTaskEntry;
                    for (int i = 0; i < garbageBoxTaskIds.Length; i++)
                    {
                        garbageBoxTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.GarbageBox.GetIntValue(),
                            EntryDataId = garbageBoxTaskIds[i],
                            StreetId = taskEntity.StreetId,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = true,
                            CompleteState = false
                        };

                        garbageBoxTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(garbageBoxTaskEntry);
                    }

                    #endregion

                    #region 小压站
                    string[] compressionStationIds = db.IQueryable<ProfileSanitationCompressionStationEntity>().Where(d => d.StreetId == taskContracts.StreetId).OrderBy(d => Guid.NewGuid()).Take(taskContracts.CompressionCount).Select(d => d.F_Id).ToArray();

                    ProfileTaskEntryEntity compressionStationTaskEntry;
                    for (int i = 0; i < compressionStationIds.Length; i++)
                    {
                        compressionStationTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.compressionStation.GetIntValue(),
                            EntryDataId = compressionStationIds[i],
                            StreetId = taskEntity.StreetId,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = true,
                            CompleteState = false
                        };

                        compressionStationTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(compressionStationTaskEntry);
                    }
                    #endregion

                    #region 沿途绿化
                    string[] greeningIds = db.IQueryable<ProfileSanitationGreeningEntity>().Where(d => d.StreetId == taskContracts.StreetId).OrderBy(d => Guid.NewGuid()).Take(taskContracts.GreeningCount).Select(d => d.F_Id).ToArray();

                    ProfileTaskEntryEntity greeningTaskEntry;

                    for (int i = 0; i < greeningIds.Length; i++)
                    {
                        greeningTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.Greening.GetIntValue(),
                            EntryDataId = greeningIds[i],
                            StreetId = taskEntity.StreetId,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = true,
                            CompleteState = false
                        };

                        greeningTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(greeningTaskEntry);
                    }

                    #endregion

                    #region 绿色账户小区
                    string[] greenResidentialIds = db.IQueryable<ProfileSanitationGreenResidentialEntity>().Where(d => d.StreetId == taskContracts.StreetId).OrderBy(d => Guid.NewGuid()).Take(taskContracts.GreenResidentialCount).Select(d => d.F_Id).ToArray();


                    ProfileTaskEntryEntity greeningResidentialTaskEntry;

                    for (int i = 0; i < greenResidentialIds.Length; i++)
                    {
                        greeningResidentialTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.GreenResidential.GetIntValue(),
                            EntryDataId = greenResidentialIds[i],
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = true,
                            CompleteState = false
                        };

                        greeningResidentialTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(greeningResidentialTaskEntry);
                    }
                    #endregion

                    //-----------非定点--------------

                    #region 废纸箱
                    ProfileTaskEntryEntity WastebasketTaskEntry;
                    for (int i = 0; i < taskContracts.WastebasketCount; i++)
                    {
                        WastebasketTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.Wastebasket.GetIntValue(),
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = false,
                            CompleteState = false
                        };

                        WastebasketTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(WastebasketTaskEntry);
                    }


                    #endregion

                    #region 沿街垃圾桶
                    ProfileTaskEntryEntity StreetTrashTaskEntry;
                    for (int i = 0; i < taskContracts.StreetTrashCount; i++)
                    {
                        StreetTrashTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.StreetTrash.GetIntValue(),
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = false,
                            CompleteState = false
                        };

                        StreetTrashTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(StreetTrashTaskEntry);
                    }

                    #endregion

                    #region 机扫车
                    ProfileTaskEntryEntity MachineCleanCarTaskEntry;
                    for (int i = 0; i < taskContracts.MachineCleanCarCount; i++)
                    {
                        MachineCleanCarTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.MachineCleanCar.GetIntValue(),
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = false,
                            CompleteState = false
                        };

                        MachineCleanCarTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(MachineCleanCarTaskEntry);
                    }

                    #endregion

                    #region 冲洗车
                    ProfileTaskEntryEntity WashTheCarTaskEntry;
                    for (int i = 0; i < taskContracts.WashTheCarCount; i++)
                    {
                        WashTheCarTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.WashTheCar.GetIntValue(),
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = false,
                            CompleteState = false
                        };

                        WashTheCarTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(WashTheCarTaskEntry);
                    }
                    
                    #endregion

                    #region 垃圾清运车

                    ProfileTaskEntryEntity GarbageTruckCarTaskEntry;
                    for (int i = 0; i < taskContracts.GarbageTruckCarCount; i++)
                    {
                        GarbageTruckCarTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.GarbageTruckCar.GetIntValue(),
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = false,
                            CompleteState = false
                        };

                        GarbageTruckCarTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(GarbageTruckCarTaskEntry);
                    }

                    #endregion

                    #region 飞行保洁车
                    ProfileTaskEntryEntity FlyingCarTaskEntry;
                    for (int i = 0; i < taskContracts.FlyingCarCount; i++)
                    {
                        FlyingCarTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.FlyingCar.GetIntValue(),
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = false,
                            CompleteState = false
                        };

                        FlyingCarTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(FlyingCarTaskEntry);
                    }

                    

                    #endregion

                    #region 四轮八桶车

                    ProfileTaskEntryEntity EightLadleCarTaskEntry;
                    for (int i = 0; i < taskContracts.EightLadleCarCount; i++)
                    {
                        EightLadleCarTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.EightLadleCar.GetIntValue(),
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = false,
                            CompleteState = false
                        };

                        EightLadleCarTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(EightLadleCarTaskEntry);
                    }

                    #endregion

                }
                else
                {
                    var taskEntity = new ProfileTaskEntity()
                    {
                        ProjectType = 1,
                        CityId = taskContracts.CityId,
                        CompanyId = taskContracts.CompanyId,
                        CountyId = taskContracts.CountyId,
                        StreetId = taskContracts.StreetId,
                        State = ProfileTaskStateEnum.NotToSend.GetIntValue(),
                        PersonInChargeId = taskContracts.PersonInChargeId,
                        CompletionTime = taskContracts.CompletionTime
                    };
                    taskEntity.Create();


                    db.Insert<ProfileTaskEntity>(taskEntity);

                    //生成检查点

                    #region 道路
                    var wayTaskEntry = new ProfileTaskEntryEntity()
                    {
                        CityId = taskEntity.CityId,
                        CompanyId = taskEntity.CompanyId,
                        CountyId = taskEntity.CountyId,
                        TaskId = taskEntity.F_Id,
                        ProjectType = 1,
                        TaskEntryType = ProfileTaskEntryTypeEnum.Way.GetIntValue(),
                        EntryDataId = taskContracts.WayId,
                        StreetId = taskEntity.StreetId,
                        F_EnCode = taskEntity.F_EnCode,
                        PersonInChargeId = taskEntity.PersonInChargeId,
                        BYMESS1 = taskContracts.WayPlaceCount,
                        IsFixedPoint = true,
                        CompleteState = false
                    };

                    wayTaskEntry.Create();

                    db.Insert<ProfileTaskEntryEntity>(wayTaskEntry);

                    #endregion

                    #region 环卫公厕


                    string[] thandasTaskIds = db.IQueryable<ProfileSanitationTandasEntity>().Where(d => d.StreetId == taskEntity.StreetId).OrderBy(d => Guid.NewGuid()).Take(taskContracts.TandasCount).Select(d => d.F_Id).ToArray();

                    ProfileTaskEntryEntity thandasTaskEntry;
                    for (int i = 0; i < thandasTaskIds.Length; i++)
                    {
                        thandasTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.Tandas.GetIntValue(),
                            EntryDataId = thandasTaskIds[i],
                            StreetId = taskEntity.StreetId,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = true,
                            CompleteState = false
                        };
                        thandasTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(thandasTaskEntry);
                    }

                    #endregion

                    #region 沿途垃圾收集设备 倒粪池 小便池

                    string[] cesspoolIds = db.IQueryable<ProfileSanitationCesspoolEntity>().Where(d => d.StreetId == taskContracts.StreetId).OrderBy(d => Guid.NewGuid()).Take(taskContracts.CesspoolCount).Select(d => d.F_Id).ToArray();

                    ProfileTaskEntryEntity cesspoolTaskEntry;
                    for (int i = 0; i < cesspoolIds.Length; i++)
                    {
                        cesspoolTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.cesspool.GetIntValue(),
                            EntryDataId = cesspoolIds[i],
                            StreetId = taskEntity.StreetId,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = true,
                            CompleteState = false
                        };

                        cesspoolTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(cesspoolTaskEntry);
                    }
                    #endregion

                    #region 垃圾箱房
                    string[] garbageBoxTaskIds = db.IQueryable<ProfileSanitationGarbageBoxEntity>().Where(d => d.StreetId == taskContracts.StreetId).OrderBy(d => Guid.NewGuid()).Take(taskContracts.GarbageBoxCount).Select(d => d.F_Id).ToArray();

                    ProfileTaskEntryEntity garbageBoxTaskEntry;
                    for (int i = 0; i < garbageBoxTaskIds.Length; i++)
                    {
                        garbageBoxTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.GarbageBox.GetIntValue(),
                            EntryDataId = garbageBoxTaskIds[i],
                            StreetId = taskEntity.StreetId,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = true,
                            CompleteState = false
                        };

                        garbageBoxTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(garbageBoxTaskEntry);
                    }

                    #endregion

                    #region 小压站
                    string[] compressionStationIds = db.IQueryable<ProfileSanitationCompressionStationEntity>().Where(d => d.StreetId == taskContracts.StreetId).OrderBy(d => Guid.NewGuid()).Take(taskContracts.CompressionCount).Select(d => d.F_Id).ToArray();

                    ProfileTaskEntryEntity compressionStationTaskEntry;
                    for (int i = 0; i < compressionStationIds.Length; i++)
                    {
                        compressionStationTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.compressionStation.GetIntValue(),
                            EntryDataId = compressionStationIds[i],
                            StreetId = taskEntity.StreetId,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = true,
                            CompleteState = false
                        };

                        compressionStationTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(compressionStationTaskEntry);
                    }
                    #endregion

                    #region 沿途绿化
                    string[] greeningIds = db.IQueryable<ProfileSanitationGreeningEntity>().Where(d => d.StreetId == taskContracts.StreetId).OrderBy(d => Guid.NewGuid()).Take(taskContracts.GreeningCount).Select(d => d.F_Id).ToArray();

                    ProfileTaskEntryEntity greeningTaskEntry;

                    for (int i = 0; i < greeningIds.Length; i++)
                    {
                        greeningTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.Greening.GetIntValue(),
                            EntryDataId = greeningIds[i],
                            StreetId = taskEntity.StreetId,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = true,
                            CompleteState = false
                        };

                        greeningTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(greeningTaskEntry);
                    }

                    #endregion

                    #region 绿色账户小区
                    string[] greenResidentialIds = db.IQueryable<ProfileSanitationGreenResidentialEntity>().Where(d => d.StreetId == taskContracts.StreetId).OrderBy(d => Guid.NewGuid()).Take(taskContracts.GreenResidentialCount).Select(d => d.F_Id).ToArray();


                    ProfileTaskEntryEntity greeningResidentialTaskEntry;

                    for (int i = 0; i < greenResidentialIds.Length; i++)
                    {
                        greeningResidentialTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.GreenResidential.GetIntValue(),
                            EntryDataId = greenResidentialIds[i],
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = true,
                            CompleteState = false
                        };

                        greeningResidentialTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(greeningResidentialTaskEntry);
                    }
                    #endregion

                    #region 废纸箱
                    ProfileTaskEntryEntity WastebasketTaskEntry;
                    for (int i = 0; i < taskContracts.WastebasketCount; i++)
                    {
                        WastebasketTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.Wastebasket.GetIntValue(),
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = false,
                            CompleteState = false
                        };

                        WastebasketTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(WastebasketTaskEntry);
                    }


                    #endregion

                    #region 沿街垃圾桶
                    ProfileTaskEntryEntity StreetTrashTaskEntry;
                    for (int i = 0; i < taskContracts.StreetTrashCount; i++)
                    {
                        StreetTrashTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.StreetTrash.GetIntValue(),
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = false,
                            CompleteState = false
                        };

                        StreetTrashTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(StreetTrashTaskEntry);
                    }

                    #endregion

                    #region 机扫车
                    ProfileTaskEntryEntity MachineCleanCarTaskEntry;
                    for (int i = 0; i < taskContracts.MachineCleanCarCount; i++)
                    {
                        MachineCleanCarTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.MachineCleanCar.GetIntValue(),
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = false,
                            CompleteState = false
                        };

                        MachineCleanCarTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(MachineCleanCarTaskEntry);
                    }

                    #endregion

                    #region 冲洗车
                    ProfileTaskEntryEntity WashTheCarTaskEntry;
                    for (int i = 0; i < taskContracts.WashTheCarCount; i++)
                    {
                        WashTheCarTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.WashTheCar.GetIntValue(),
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = false,
                            CompleteState = false
                        };

                        WashTheCarTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(WashTheCarTaskEntry);
                    }

                    #endregion

                    #region 垃圾清运车

                    ProfileTaskEntryEntity GarbageTruckCarTaskEntry;
                    for (int i = 0; i < taskContracts.GarbageTruckCarCount; i++)
                    {
                        GarbageTruckCarTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.GarbageTruckCar.GetIntValue(),
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = false,
                            CompleteState = false
                        };

                        GarbageTruckCarTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(GarbageTruckCarTaskEntry);
                    }

                    #endregion

                    #region 飞行保洁车
                    ProfileTaskEntryEntity FlyingCarTaskEntry;
                    for (int i = 0; i < taskContracts.FlyingCarCount; i++)
                    {
                        FlyingCarTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.FlyingCar.GetIntValue(),
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = false,
                            CompleteState = false
                        };

                        FlyingCarTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(FlyingCarTaskEntry);
                    }



                    #endregion

                    #region 四轮八桶车

                    ProfileTaskEntryEntity EightLadleCarTaskEntry;
                    for (int i = 0; i < taskContracts.EightLadleCarCount; i++)
                    {
                        EightLadleCarTaskEntry = new ProfileTaskEntryEntity()
                        {
                            CityId = taskEntity.CityId,
                            CompanyId = taskEntity.CompanyId,
                            CountyId = taskEntity.CountyId,
                            TaskId = taskEntity.F_Id,
                            ProjectType = 1,
                            TaskEntryType = ProfileTaskEntryTypeEnum.EightLadleCar.GetIntValue(),
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId,
                            IsFixedPoint = false,
                            CompleteState = false
                        };

                        EightLadleCarTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(EightLadleCarTaskEntry);
                    }

                    #endregion

                }

                db.Commit();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(ProfileTaskEntity taskEntuty)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {

                if (taskEntuty.State != ProfileTaskStateEnum.NotToSend.GetIntValue())
                {
                    throw new Exception("任务但只有未派遣状态下才允许删除操作!");
                }

                db.Delete<ProfileTaskEntity>(taskEntuty);

                //删除子表
                var deleteTaskEntrys = db.IQueryable<ProfileTaskEntryEntity>().Where(d => d.TaskId == taskEntuty.F_Id).ToArray();
                foreach (var item in deleteTaskEntrys)
                {
                    db.Delete<ProfileTaskEntryEntity>(item);
                }

                db.Commit();
            }
        }

        public void TaskDistributed(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                var taskEntity = db.FindEntity<ProfileTaskEntity>(keyValue);
                if (taskEntity.State != ProfileTaskStateEnum.NotToSend.GetIntValue())
                {
                    throw new Exception("当前任务单已经下发过了，请勿重复操作。");
                }

                taskEntity.Modify(taskEntity.F_Id);

                taskEntity.DeliveryTime = DateTime.Now;//设置下发时间
                taskEntity.F_EnCode = ((DateTime)taskEntity.DeliveryTime).ToString("yyyyMMddHHmmss");//任务单号根据派发时间生成
                taskEntity.State = ProfileTaskStateEnum.ToAudit.GetIntValue();

                db.Update<ProfileTaskEntity>(taskEntity);

                db.Commit();
            }
        }

        /// <summary>
        /// 任务单作废
        /// </summary>
        /// <param name="keyValue"></param>
        public void TaskInvalid(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {

                var taskEntity = db.FindEntity<ProfileTaskEntity>(keyValue);
                if (taskEntity.State == ProfileTaskStateEnum.HavePutAnEndTo.GetIntValue())
                {
                    throw new Exception("当前任务单是完结状态，完结状态下不可作废操作。");
                }

                taskEntity.Modify(taskEntity.F_Id);

                taskEntity.State = ProfileTaskStateEnum.TheCancellation.GetIntValue();

                db.Update<ProfileTaskEntity>(taskEntity);


                db.Commit();
            }
        }

        public void Command<TEntity>(Action<IQueryable<TEntity>> callBack) where TEntity : class
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                callBack(db.IQueryable<TEntity>());
            }
        }
    }
}
