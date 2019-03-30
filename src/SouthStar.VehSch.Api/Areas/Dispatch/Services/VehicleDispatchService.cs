using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Common.Dapper;
using OneZero.Common.Dtos;
using OneZero.Common.Enums;
using OneZero.Common.Exceptions;
using OneZero.Common.Extensions;
using OneZero.Domain.Repositories;
using OneZero.Application;
using SouthStar.VehSch.Api.Areas.ApplicationFlow.Dtos;
using SouthStar.VehSch.Api.Areas.ApplicationFlow.Models;
using SouthStar.VehSch.Api.Areas.ApplicationFlow.Models.Enum;
using SouthStar.VehSch.Api.Areas.Dispatch.Dtos;
using SouthStar.VehSch.Api.Areas.Dispatch.Models;
using SouthStar.VehSch.Api.Areas.Setting.Models;
using SouthStar.VehSch.Api.EventBues.DispatchVehilceEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Dispatch.Services
{
    public class VehicleDispatchService : BaseService
    {
        private ILogger<VehicleDispatchService> _logger;
        private readonly OutputDto output = new OutputDto();
        private IRepository<VehicleApplications, Guid> _applyRepository;
        private IRepository<CheckContents, Guid> _checkRepository;
        private IRepository<VehicleDispatchs, Guid> _dispatchRepository;
        private IRepository<Departments, Guid> _departmentRepository;
        private IMediator _mediator;

        public VehicleDispatchService(IUnitOfWork unitOfWork, ILogger<VehicleDispatchService> logger, IDapperProvider dapper, IMapper mapper, IMediator mediator) : base(unitOfWork, dapper, mapper)
        {
            _applyRepository = unitOfWork.Repository<VehicleApplications, Guid>();
            _checkRepository = unitOfWork.Repository<CheckContents, Guid>();
            _departmentRepository = unitOfWork.Repository<Departments, Guid>();
            _dispatchRepository = unitOfWork.Repository<VehicleDispatchs, Guid>();
            _logger = logger;
            _mediator = mediator;
        }


        /// <summary>
        /// 查询派车列表
        /// </summary>
        /// <param name="applicantId">申请人Id</param>
        /// <param name="status">申请状态</param>
        /// <param name="applyNum">申请编号</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetVehicleDispatchListAsync(string applyNum, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int limit = 20)
        {
            int skipCount = 0;
            //找到申请状态为已审核且审核状态为通过或者已派车的申请单
            var apply = from a in _applyRepository.Entities.Where(v => (string.IsNullOrWhiteSpace(applyNum) || EF.Functions.Like(v.ApplyNum, "%" + applyNum + "%")
                                                                    && (v.Status == ApplyState.Checked)                           //不能是起草的申请
                                                                    && (startDate == null || v.CreateDate >= startDate)
                                                                    && (endDate == null || v.CreateDate <= endDate))).OrderBy(v => v.ApplyNum)
                        join b in _checkRepository.Entities.Where(v => v.CheckStatus == CheckStatus.Approved || v.CheckStatus == CheckStatus.Dispatched) on a.Id equals b.ApplyId
                        select new { a.Id };

            var sumCount = await apply.CountAsync();
            if (sumCount <= 0)
                return output;

            output.PageInfo = Paging(page, limit, sumCount, out skipCount);
            if (skipCount < 0 || output.PageInfo == null)
                return output;

            var query = from p in apply.Skip(skipCount).Take(limit)
                        join a in _applyRepository.Entities on p.Id equals a.Id
                        join ds in _dispatchRepository.Entities on a.Id equals ds.ApplyId into ds_join
                        from dsi in ds_join.DefaultIfEmpty()
                        join d in _departmentRepository.Entities on a.DepartmentId equals d.Id into d_join
                        from di in d_join.DefaultIfEmpty()
                        join r in _checkRepository.Entities.Where(v => v.CheckStatus == CheckStatus.Approved || v.CheckStatus == CheckStatus.Dispatched) on a.Id equals r.ApplyId
                        select new VehicleDispatchListData()
                        {
                            Id = dsi.Id == default(Guid) ? (Guid?)null : dsi.Id,
                            ApplicantName = a.ApplicantName,
                            ApplyId = a.Id,
                            ApplyNum = a.ApplyNum,
                            ApplyReson = a.ApplyReson,
                            BackPlanTime = a.BackPlanTime,
                            StartPlanTime = a.StartPlanTime,
                            Status = "",
                            UseArea = a.UseArea,
                            UserName = a.UserName,
                            UserMobile = a.UserMobile,
                            CreateDate = r.CreateDate,
                            CarType = a.CarType,
                            CarProperty = a.CarProperty.GetRemark(),
                            CheckStatus = r.CheckStatus.GetRemark(),
                            DepartmentName = di.DepartmentName,
                            Destination = a.Destination,
                            StartPoint = a.StartPoint,
                            DriverName = dsi.DriverName,
                            PlateNumber = dsi.PlateNumber
                        };
            output.Datas = await query?.ToListAsync();
            return output;
        }

        /// <summary>
        /// GetItemAsync
        /// </summary>
        /// <param name="dispatchId"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetItemAsync(Guid dispatchId)
        {
            var dispatch = await _dispatchRepository.Entities.FirstOrDefaultAsync(v => (v.Id.Equals(dispatchId)));

            if (dispatch != null)
            {
                output.Datas = new List<VehicleDispatchData> { ConvertToDataDto<VehicleDispatchs, VehicleDispatchData>(dispatch) };
            }
            return output;
        }


        /// <summary>
        /// 生成派车单
        /// </summary>
        /// <param name="applyId"></param>
        /// <param name="dispatchPostData"></param>
        /// <returns></returns>
        public async Task<OutputDto> AddDispatchAsync(Guid applyId, VehicleDispatchPostData dispatchPostData)
        {
            var apply = await _applyRepository.Entities.FirstOrDefaultAsync(v => (v.Id.Equals(applyId)));

            if (apply == null)
                throw new OneZeroException("申请信息异常，生成派车单失败", ResponseCode.ExpectedException);

            //生产派车单
            Guid newId = GuidHelper.NewGuid();
            var dispatch = new VehicleDispatchs()
            {
                Id = newId,
                ApplyId = applyId,
                ApplicantName = apply.ApplicantName,
                ApplicantPhone = apply.ApplicantPhone,
                ApplyNum = apply.ApplyNum,
                ApplyReson = apply.ApplyReson,
                BackPlanTime = apply.BackPlanTime,
                CarType = apply.CarType,
                CarProperty = apply.CarProperty,
                ApplyDate = apply.CreateDate,
                Destination = apply.Destination,
                UserMobile = apply.UserMobile,
                StartPlanTime = apply.StartPlanTime,
                StartPoint = apply.StartPoint,
                UserDepartment = apply.DepartmentName,
                DriverPhone = "",//GetDriverPhone(dispatchPostData.DriverId),
                UserName = apply.UserName,
                UserCount = apply.UserCount,
                UserTitle = apply.UserTitle,
                DriverId = dispatchPostData.DriverId,
                DriverName = dispatchPostData.DriverName,
                VehcileId = dispatchPostData.VehicleId,
                PlateNumber = dispatchPostData.PlateNumber,
                CreateDate = DateTime.Now,
                DispatchType = dispatchPostData.DispatchType,
                QueueNo = dispatchPostData.QueueNo ?? -1
            };
            var result = await _dispatchRepository.AddAsync(dispatch);
            if (result <= 0)
                throw new OneZeroException("生成派车单失败：更新其他相关状态出现错误！", ResponseCode.UnExpectedException);

            //触发订阅了DispatchVehicleEventArgs的事件
            var eventArgs = CreateDispatchVehicleEventArgs(newId, dispatchPostData, applyId);
            await _mediator.Publish(eventArgs);
            output.Message = "生成派车单成功";
            return output;
        }

        /// <summary>
        /// 重新调度
        /// </summary>
        /// <param name="applyId"></param>
        /// <param name="dispatchPostData"></param>
        /// <returns></returns>
        public async Task<OutputDto> UpdateDispatchAsync(Guid applyId, VehicleDispatchPostData dispatchPostData)
        {
            var dispatch = await _dispatchRepository.Entities.FirstOrDefaultAsync(v => v.ApplyId == applyId);
            if (dispatch != null && dispatchPostData != null)
                throw new OneZeroException("重新调度：更新派车单失败！", ResponseCode.ExpectedException);

            dispatch.DriverId = dispatchPostData.DriverId;
            dispatch.DriverName = dispatchPostData.DriverName;
            dispatch.DispatchType = dispatchPostData.DispatchType;
            dispatch.QueueNo = dispatchPostData.QueueNo ?? -1;
            dispatch.PlateNumber = dispatchPostData.PlateNumber;
            dispatch.VehcileId = dispatchPostData.VehicleId;
            dispatch.DispatchType = dispatchPostData.DispatchType;
            await _dispatchRepository.UpdateAsync(dispatch);
            output.Message = "重新调度：成功！";

            //触发订阅了DispatchVehicleEventArgs的事件
            var eventArgs = CreateDispatchVehicleEventArgs(dispatch.Id, dispatchPostData);
            await _mediator.Publish(eventArgs);

            return output;
        }


        private DispatchVehicleEventArgs CreateDispatchVehicleEventArgs(Guid DispatchId, VehicleDispatchPostData dispatchPostData,Guid applyId=default(Guid))
        {
            return new DispatchVehicleEventArgs()
            {
                ApplyId= applyId,
                DispatchId = DispatchId,
                DriverId = dispatchPostData.DriverId,
                DriverStatus = Common.Enum.PersonState.OnWork,
                EventDate = DateTime.Now,
                QueueNo = dispatchPostData.QueueNo,
                VehicleId = dispatchPostData.VehicleId,
                VehicleStatus = Setting.Models.Enum.CurrentState.OnDuty,
                QueueId = dispatchPostData.QueueId
            };
        }
    }
}
