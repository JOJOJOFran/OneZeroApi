using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Common.Dapper;
using OneZero.Dtos;
using OneZero.Enums;
using OneZero.Exceptions;
using OneZero.Common.Extensions;
using OneZero.Domain;
using OneZero.Core;
using SouthStar.VehSch.Core.ApplicationFlow.Dtos;
using SouthStar.VehSch.Core.ApplicationFlow.Models;
using SouthStar.VehSch.Core.Dispatch.Dtos;
using SouthStar.VehSch.Core.Dispatch.Models;
using SouthStar.VehSch.Core.Setting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneZero.Common.Helpers;
using SouthStar.VehSch.Core.Common.Enums;
using SouthStar.VehSch.Core.EventBues.DispatchVehilceEvent;
using SouthStar.VehSch.Core.Dispatchs.Dtos;
using OneZero.Options;
using OneZero;

namespace SouthStar.VehSch.Core.Dispatch.Services
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
        private readonly OneZeroOption _oneZeroOption;
        private readonly OneZeroContext _oneZeroContext;

        public VehicleDispatchService(IUnitOfWork unitOfWork, ILogger<VehicleDispatchService> logger, IDapperProvider dapper, IMapper mapper, IMediator mediator, OneZeroOption oneZeroOption, OneZeroContext oneZeroContext) : base(unitOfWork, dapper, mapper)
        {
            _applyRepository = unitOfWork.Repository<VehicleApplications, Guid>();
            _checkRepository = unitOfWork.Repository<CheckContents, Guid>();
            _departmentRepository = unitOfWork.Repository<Departments, Guid>();
            _dispatchRepository = unitOfWork.Repository<VehicleDispatchs, Guid>();
            _logger = logger;
            _mediator = mediator;
            _oneZeroOption = oneZeroOption;
            _oneZeroContext = oneZeroContext;
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
            var apply = from a in _applyRepository.Entities.Where(v => (string.IsNullOrWhiteSpace(applyNum) || EF.Functions.Like(v.ApplyNum, "%" + applyNum + "%"))
                                                                    && (v.Status == ApplyState.Checked)                           //不能是起草的申请
                                                                    && (startDate == null || v.CreateDate >= startDate)
                                                                    && (endDate == null || v.CreateDate <= endDate)).OrderBy(v => v.ApplyNum)
                        join b in _checkRepository.Entities.Where(v => v.CheckStatus == CheckStatus.Approved || v.CheckStatus == CheckStatus.Dispatched) on a.Id equals b.ApplyId
                        select new { a.Id };

            var sumCount = await apply.CountAsync();
            if (sumCount <= 0)
                return output;

            output.PageInfo = Paging(page, limit, sumCount, out skipCount);
            if (skipCount < 0 || output.PageInfo == null)
                return output;

            var test = apply.Skip(skipCount).Take(limit);
            var query = from p in apply.Skip(skipCount).Take(limit)
                        join a in _applyRepository.Entities on p.Id equals a.Id
                        join ds in _dispatchRepository.Entities on a.Id equals ds.ApplyId into ds_join
                        from dsi in ds_join.DefaultIfEmpty()
                        join d in _departmentRepository.Entities on a.DepartmentId equals d.Id into d_join
                        from di in d_join.DefaultIfEmpty()
                        join r in _checkRepository.Entities.Where(v => v.CheckStatus == CheckStatus.Approved || v.CheckStatus == CheckStatus.Dispatched) on a.Id equals r.ApplyId
                        select new 
                        {
                            Id = dsi.Id == default(Guid) ? (Guid?)null : dsi.Id,
                            ApplicantName = a.ApplicantName,
                            ApplyId = a.Id,
                            ApplyNum = a.ApplyNum,
                            ApplyReson = a.ApplyReson,
                            BackPlanTime = a.BackPlanTime,
                            StartPlanTime = a.StartPlanTime,
                            Status = a.Status.GetRemark(),
                            UseArea = a.UseArea,
                            UserName = a.UserName,
                            UserMobile = a.UserMobile,
                            CreateDate = r.CreateDate,
                            CarType = a.CarType,
                            CarProperty = a.CarProperty.GetRemark(),
                            CheckStatus = r.CheckStatus== CheckStatus.Approved?"待调度":"已调度",
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
        /// 获取已派车
        /// </summary>
        /// <param name="applyNum"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetDispatchedListAsync(string applyNum, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int limit = 20)
        {
            int skipCount = 0;
            //找到申请状态为已审核且审核状态为通过或者已派车的申请单
            var apply = _dispatchRepository.Entities.Where(v => (string.IsNullOrWhiteSpace(applyNum) || EF.Functions.Like(v.ApplyNum, "%" + applyNum + "%")

                                                                    && (startDate == null || v.CreateDate >= startDate)
                                                                    && (endDate == null || v.CreateDate <= endDate))).OrderBy(v => v.ApplyNum).Select(v => new { v.Id });

            var sumCount = await apply.CountAsync();
            if (sumCount <= 0)
                return output;

            output.PageInfo = Paging(page, limit, sumCount, out skipCount);
            if (skipCount < 0 || output.PageInfo == null)
                return output;

            var test = apply.Skip(skipCount).Take(limit);
            var query = from p in apply.Skip(skipCount).Take(limit)
                        join a in _dispatchRepository.Entities on p.Id equals a.Id
                        //join ds in _dispatchRepository.Entities on a.Id equals ds.ApplyId into ds_join
                        //from dsi in ds_join.DefaultIfEmpty()      
                       // join r in _checkRepository.Entities.Where(v => v.CheckStatus == CheckStatus.Approved || v.CheckStatus == CheckStatus.Dispatched) on a.Id equals r.ApplyId
                        select new
                        {
                            Id = a.Id,
                            ApplicantName = a.ApplicantName,
                            ApplyId = a.ApplyId,
                            ApplyNum = a.ApplyNum,
                            ApplyReson = a.ApplyReson,
                            BackPlanTime = a.BackPlanTime,
                            StartPlanTime = a.StartPlanTime,
                            //Status = a.Status.GetRemark(),
                            UseArea = a.UseArea,
                            UserName = a.UserName,
                            UserMobile = a.UserMobile,
                            CreateDate = a.CreateDate,
                            CarType = a.CarType,
                            CarProperty = a.CarProperty.GetRemark(),
                           // CheckStatus = r.CheckStatus.GetRemark(),
                            DepartmentName = a.UserDepartment,
                            Destination = a.Destination,
                            StartPoint = a.StartPoint,
                            DriverName = a.DriverName,
                            PlateNumber = a.PlateNumber
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
                DriverPhone = dispatchPostData.DriverPhone,
                UserName = apply.UserName,
                UseArea=apply.UseArea,
                UserCount = apply.UserCount,
                UserTitle = apply.UserTitle,
                DriverId = dispatchPostData.DriverId,
                DriverName = dispatchPostData.DriverName,
                VehcileId = dispatchPostData.VehicleId,
                PlateNumber = dispatchPostData.PlateNumber,
                CreateDate = DateTime.Now,
                DispatchType = dispatchPostData.DispatchType,
                QueueNo = dispatchPostData.QueueNo ?? -1,
                TenanId = _oneZeroContext.TenanId
            };
            var result = await _dispatchRepository.AddAsync(dispatch);
            if (result <= 0)
                throw new OneZeroException("生成派车单失败！", ResponseCode.UnExpectedException);

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


        /// <summary>
        /// 快速调度派车(不走申请，直接手动调度派车)
        /// </summary>
        /// <param name="quickDispatch"></param>
        /// <returns></returns>
        public async Task<OutputDto> QuickDispatchAsync(QuickDispatchPostData quickDispatch)
        {
            try
            {
                await _unitOfWork.BeginTransAsync();
                //生成申请单
                var apply = await CreateVehicleApplication(quickDispatch);
                var result1 = await _applyRepository.AddAsync(apply);
                //生成审核单
                var check = await CreateCheckContens(apply);
                var result3= await  _checkRepository.AddAsync(check);
                //生成派车单
                var dispatch = await CreateVehicleDispatchs(apply, quickDispatch);
                var result2 = await _dispatchRepository.AddAsync(dispatch);
                output.Message = "操作完成";

                if (result1 < 1|| result2 < 1||result3 < 1)
                {
                    await _unitOfWork.RollbackAsync();
                    output.Message = "提交失败,请重试!";
                }
                else
                {
                    await _unitOfWork.CommitAsync();
                    var eventArgs = CreateQuickDispatchEventArgs(dispatch.Id, quickDispatch, apply.Id);
                    await _mediator.Publish(eventArgs);
                }
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogWarning(e, "快速派单失败");
                output.Message = "提交失败";
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return output;
        }

        /// <summary>
        /// 创建派车单
        /// </summary>
        /// <param name="apply"></param>
        /// <param name="quickDispatch"></param>
        /// <returns></returns>
        private async Task<VehicleDispatchs> CreateVehicleDispatchs(VehicleApplications apply, QuickDispatchPostData quickDispatch)
        {
            var result= new VehicleDispatchs()
            {
                Id = GuidHelper.NewGuid(),
                ApplyId = apply.Id,
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
                DriverPhone = quickDispatch.DriverPhone,
                UserName = apply.UserName,
                UserCount = apply.UserCount,
                UserTitle = apply.UserTitle,
                DriverId = quickDispatch.DriverId,
                DriverName = quickDispatch.DriverName,
                VehcileId = quickDispatch.VehicleId,
                PlateNumber = quickDispatch.PlateNumber,
                CreateDate = DateTime.Now,
                DispatchType = DispatchType.Manually,
                TenanId = _oneZeroContext.TenanId,
                IsDelete = default(Guid)
            };
            return await Task.FromResult(result);
        }



        private async Task<CheckContents> CreateCheckContens(VehicleApplications apply)
        {
            var result= new CheckContents()
            {
                Id = GuidHelper.NewGuid(),
                ApplyId = apply.Id,
                ApplyNum = apply.ApplyNum,
                ApplyType = ApplyType.VehicleApply,
                CheckStatus = CheckStatus.Dispatched,
                CheckUserId = apply.ApplicantId,
                CheckUserName = apply.ApplicantName,
                CreateDate = DateTime.Now,
                CheckReply = "同意",
                IsDelete = default(Guid),
                TenanId = _oneZeroContext.TenanId
            };

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 创建申请单
        /// </summary>
        /// <param name="quickDispatch"></param>
        /// <returns></returns>
        private async Task<VehicleApplications> CreateVehicleApplication(QuickDispatchPostData quickDispatch)
        {
            return new VehicleApplications
            {
                Id = GuidHelper.NewGuid(),
                ApplicantId = quickDispatch.ApplicantId,
                ApplicantName = quickDispatch.ApplicantName,
                ApplyNum = await GetNextSeq(),
                ApplyReson = quickDispatch.ApplyReson,
                BackPlanTime = quickDispatch.BackPlanTime,
                CarProperty = quickDispatch.CarProperty,
                CarType = quickDispatch.CarType,
                CreateDate = DateTime.Now,
                DepartmentId = quickDispatch.DepartmentId,
                DepartmentName = quickDispatch.DepartmentName,
                Destination = quickDispatch.Destination,
                StartPlanTime = quickDispatch.StartPlanTime,
                StartPoint = quickDispatch.StartPoint,
                Status = ApplyState.Checked,
                Remark = quickDispatch.Remark,
                UserCount = quickDispatch.UserCount,
                UserName = quickDispatch.UserName,
                UserMobile = quickDispatch.UserMobile,
                IsDelete = default(Guid),
                TenanId = _oneZeroContext.TenanId,
            };
        }

        /// <summary>
        /// 生成派车事件载体
        /// </summary>
        /// <param name="DispatchId"></param>
        /// <param name="dispatchPostData"></param>
        /// <param name="applyId"></param>
        /// <returns></returns>
        private DispatchVehicleEventArgs CreateDispatchVehicleEventArgs(Guid DispatchId, VehicleDispatchPostData dispatchPostData, Guid applyId = default(Guid))
        {
            return new DispatchVehicleEventArgs()
            {
                ApplyId = applyId,
                DispatchId = DispatchId,
                DriverId = dispatchPostData.DriverId,
                DriverStatus = PersonState.OnWork,
                EventDate = DateTime.Now,
                QueueNo = dispatchPostData.QueueNo,
                VehicleId = dispatchPostData.VehicleId,
                VehicleStatus = CurrentState.OnDuty,
                QueueId = dispatchPostData.QueueId
            };
        }

        /// <summary>
        /// 生成派车事件载体
        /// </summary>
        /// <param name="DispatchId"></param>
        /// <param name="dispatchPostData"></param>
        /// <param name="applyId"></param>
        /// <returns></returns>
        private QuickDispatchEventArgs CreateQuickDispatchEventArgs(Guid DispatchId, QuickDispatchPostData dispatchPostData, Guid applyId = default(Guid))
        {
            return new QuickDispatchEventArgs()
            {
                ApplyId = applyId,
                DispatchId = DispatchId,
                DriverId = dispatchPostData.DriverId,
                DriverStatus = PersonState.OnWork,
                EventDate = DateTime.Now,
                VehicleId = dispatchPostData.VehicleId,
                VehicleStatus = CurrentState.OnDuty,
                DispatchType = DispatchType.Manually,
            };
        }

        /// <summary>
        /// 生成流水号
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetNextSeq()
        {

            var apply = await _applyRepository.NoFilterEntities.OrderByDescending(v => v.ApplyNum).FirstOrDefaultAsync();
            //当申请单为空，或者最大编号的年份小于当前年份（说明跨年了)的时候流水后四位从“0000”开始
            if (apply == null || Convert.ToInt32(apply.ApplyNum?.Substring(0, 4)) < DateTime.Now.Year)
            {
                return DateTime.Now.ToString("yyyyMMdd") + "0000";
            }
            else
            {
                return DateTime.Now.ToString("yyyyMMdd") + (Int64.Parse(apply.ApplyNum) + 1).ToString().Substring(8, 4);
            }

        }
    }
}
