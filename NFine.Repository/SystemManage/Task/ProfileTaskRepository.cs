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
                        PersonInChargeId = taskEntity.PersonInChargeId
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
                            PersonInChargeId = taskEntity.PersonInChargeId
                        };
                        thandasTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(thandasTaskEntry);
                    }

                    #endregion

                    #region 沿途垃圾收集设备 暂时没写

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
                            PersonInChargeId = taskEntity.PersonInChargeId
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
                            PersonInChargeId = taskEntity.PersonInChargeId
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
                            PersonInChargeId = taskEntity.PersonInChargeId
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
                            TaskEntryType = ProfileTaskEntryTypeEnum.Greening.GetIntValue(),
                            EntryDataId = greenResidentialIds[i],
                            StreetId = taskEntity.StreetId,
                            F_EnCode = taskEntity.F_EnCode,
                            PersonInChargeId = taskEntity.PersonInChargeId
                        };

                        greeningResidentialTaskEntry.Create();

                        db.Insert<ProfileTaskEntryEntity>(greeningResidentialTaskEntry);
                    }
                    #endregion

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

                taskEntity.DeliveryTime = DateTime.Now;//设置下发时间
                taskEntity.F_EnCode = ((DateTime)taskEntity.DeliveryTime).ToString("yyyyMMddHHmmss");//任务单号根据派发时间生成
                taskEntity.State = ProfileTaskStateEnum.HasSent.GetIntValue();

                db.Update<ProfileTaskEntity>(taskEntity);

                db.Commit();
            }
        }
    }
}
