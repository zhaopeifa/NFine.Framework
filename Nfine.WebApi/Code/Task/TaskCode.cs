using NFine.Application.SystemManage;
using NFine.Data;
using NFine.Data.Extensions;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.Enums;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nfine.WebApi.Code.Task
{
    public class TaskCode : ITask
    {
        private ProfileTaskRepository Taskservice = new ProfileTaskRepository();

        public Contracts.ApiWayContracts[] GetWayTask(string UserId, Data.Enums.CheckPointClassificationEnum type)
        {
            #region 变量

            DateTime currentTime = DateTime.Now;
            int hasSentState = ProfileTaskStateEnum.HasSent.GetIntValue();

            //当前道路的code
            int dataTypeCode = ProfileTaskEntryTypeEnum.Way.GetIntValue();
            //当前获取道路类型转换为道路的
            int wayTypeCode = Unti.CheckPointAssociatedExtensions.GetWayGradeAssociated(type);
            #endregion

            //寻找当前外勤人员是否存在要完成的任务
            var taskIsHave = from taskQ in LinqSQLExtensions.IQueryable<ProfileTaskEntity>()
                             where taskQ.PersonInChargeId == UserId &&
                              taskQ.DeliveryTime <= currentTime &&
                              taskQ.CompletionTime >= currentTime &&
                              taskQ.State == hasSentState
                             select new
                             {
                                 f_id = taskQ.F_Id
                             };

            if (taskIsHave.Count() <= 0)
            {
                throw new Exception("此用户当前未发现任何任务!");
            }

            //查道路任务

            var taskQuery = from taskQ in LinqSQLExtensions.IQueryable<ProfileTaskEntity>()
                            join taskEntryQ in LinqSQLExtensions.IQueryable<ProfileTaskEntryEntity>()
                            on taskQ.F_Id equals taskEntryQ.TaskId
                            where taskQ.PersonInChargeId == UserId &&
                            taskQ.State == hasSentState &&
                            taskQ.DeliveryTime <= currentTime &&
                            taskQ.CompletionTime >= currentTime &&
                            taskEntryQ.TaskEntryType == dataTypeCode
                            select new
                            {
                                TaskId = taskQ.F_Id,
                                TaskEntryId = taskEntryQ.F_Id,
                                DeliveryTime = taskQ.DeliveryTime,
                                CompletionTime = taskQ.CompletionTime,
                                EntryDataId = taskEntryQ.EntryDataId
                            };

            var dataQuery = from taskq in taskQuery
                            join wayData in LinqSQLExtensions.IQueryable<ProfileSanitationWayEntity>()
                            on taskq.EntryDataId equals wayData.F_Id
                            where wayData.WayGrade == wayTypeCode
                            select new
                            {
                                TaskId = taskq.TaskId,
                                TaskEntryId = taskq.TaskEntryId,
                                Origin = wayData.Origin,
                                Destination = wayData.Destination,
                                DeliveryTime = taskq.DeliveryTime,
                                CompletionTime = taskq.CompletionTime,
                                StreetId = wayData.StreetId
                            };

            var StreetQuery = from dataQ in dataQuery
                              join streetQ in LinqSQLExtensions.IQueryable<ProfileStreetEntity>()
                              on dataQ.StreetId equals streetQ.F_Id
                              select new
                              {
                                  TaskId = dataQ.TaskId,
                                  TaskEntryId = dataQ.TaskEntryId,
                                  Origin = dataQ.Origin,
                                  Destination = dataQ.Destination,
                                  DeliveryTime = dataQ.DeliveryTime,
                                  CompletionTime = dataQ.CompletionTime,
                                  StreetId = dataQ.StreetId,
                                  StreetName = streetQ.StreetName
                              };

            return StreetQuery.Select(d => new Contracts.ApiWayContracts()
            {
                Title = "道路清扫",
                Id = d.TaskId,
                CompletionTime = (DateTime)d.DeliveryTime,
                DeliveryTime = d.CompletionTime,
                Name = d.Origin + "--" + d.Destination,
                StreetId = d.StreetId,
                StreetName = d.StreetName,
                IsComplete = 0
            }).ToArray();
        }
    }
}