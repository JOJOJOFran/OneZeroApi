using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Common.Dapper;
using OneZero.Common.Dtos;
using OneZero.Common.Enums;
using OneZero.Common.Exceptions;
using OneZero.Common.Extensions;
using OneZero.Domain.Repositories;
using SouthStar.VehSch.Api.Areas.ApplicationFlow.Dtos;
using SouthStar.VehSch.Api.Areas.ApplicationFlow.Models;
using SouthStar.VehSch.Api.Areas.ApplicationFlow.Models.Enum;
using SouthStar.VehSch.Api.Areas.Setting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.ApplicationFlow.Services
{
    public class CheckService : BaseService
    {
        private ILogger<VehicleApplyService> _logger;
        private readonly OutputDto output = new OutputDto();
        private IRepository<VehicleApplications, Guid> _applyRepository;
        private IRepository<CheckContents, Guid> _checkRepository;
        private IRepository<Departments, Guid> _departmentRepository;

        public CheckService(IUnitOfWork unitOfWork, ILogger<VehicleApplyService> logger, IDapperProvider dapper, IMapper mapper) : base(unitOfWork, dapper, mapper)
        {
            _applyRepository = unitOfWork.Repository<VehicleApplications, Guid>();
            _checkRepository = unitOfWork.Repository<CheckContents, Guid>();
            _departmentRepository = unitOfWork.Repository<Departments, Guid>();
            _logger = logger;
        }

        /// <summary>
        /// 查询用车申请列表
        /// </summary>
        /// <param name="applicantId">申请人Id</param>
        /// <param name="status">申请状态</param>
        /// <param name="applyNum">申请编号</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetVehicleApplyListAsync(ApplyState? status, string applyNum, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int limit = 20)
        {
            int skipCount = 0;

            var apply = _applyRepository.Entities.Where(v => (string.IsNullOrWhiteSpace(applyNum) || EF.Functions.Like(v.ApplyNum, "%" + applyNum + "%")
                                                                    && (status == null || v.Status.Equals(status))
                                                                    && (v.Status != ApplyState.Draft)                           //不能是起草的申请
                                                                    && (startDate == null || v.CreateDate >= startDate)
                                                                    && (endDate == null || v.CreateDate <= endDate))).OrderBy(v => v.ApplyNum);
            var sumCount = await apply.Select(v => new { v.Id }).CountAsync();
            if (sumCount <= 0)
                return output;

            output.PageInfo = Paging(page, limit, sumCount, out skipCount);
            if (skipCount < 0 || output.PageInfo == null)
                return output;

            var query = from a in apply.Skip(skipCount).Take(limit)
                        join d in _departmentRepository.Entities on a.DepartmentId equals d.Id into d_join
                        from di in d_join.DefaultIfEmpty()
                        join r in _checkRepository.Entities on a.Id equals r.ApplyId
                        select new VehicleApplicationListData()
                        {
                            ApplicantName = a.ApplicantName,
                            Id = a.Id,
                            ApplyNum = a.ApplyNum,
                            ApplyReson = a.ApplyReson,
                            ApplicantPhone = a.ApplicantPhone,
                            BackPlanTime = a.BackPlanTime,
                            StartPlanTime = a.StartPlanTime,
                            Status = "", //a.Status.GetRemark(),
                            UseArea = a.UseArea,
                            UserName = a.UserName,
                            UserMobile = a.UserMobile,
                            CarType = a.CarType,
                            CarProperty = a.CarProperty.GetRemark(),
                            CheckStatus = r.CheckStatus.GetRemark(),
                            DepartmentName = String.IsNullOrEmpty(di.DepartmentName) ? "-" : di.DepartmentName,
                            Destination = a.Destination,
                            StartPoint = a.StartPoint,
                        };
            output.Datas = await query?.ToListAsync();
            return output;
        }


        /// <summary>
        /// 获取审核信息
        /// </summary>
        /// <param name="applyId">审核ID</param>
        /// <returns></returns>
        public async Task<OutputDto> GetCheckItemAsync(Guid checkId)
        {
            var check = await _checkRepository.Entities.FirstOrDefaultAsync(v => v.Id.Equals(checkId));
            output.Datas = new List<CheckContentData> { ConvertToDataDto<CheckContents, CheckContentData>(check) };
            return output;
        }

        /// <summary>
        /// 获取用车申请审核详细信息
        /// </summary>
        /// <param name="checkId"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetVehicleApplyCheckItemAsync(Guid checkId)
        {
            var query = from c in _checkRepository.Entities.Where(v => v.Id.Equals(checkId)&&v.ApplyType.Equals(ApplyType.VehicleApply))
                        from a in _applyRepository.Entities.Where(v=>v.Status!=(ApplyState.Draft))
                        from d in _departmentRepository.Entities
                        where (a.Id==c.ApplyId) && (a.ApplyNum==c.ApplyNum)&&(a.DepartmentId==d.Id)
                        select new VehicleApplyCheckData
                        {
                            Id = c.Id,
                            ApplyId =a.Id,
                            ApplicantName = a.ApplicantName,
                            ApplicantPhone = a.ApplicantPhone,
                            UserMobile = a.UserMobile,
                            ApplyNum = a.ApplyNum,
                            ApplyReson = a.ApplyReson,
                            BackPlanTime = a.BackPlanTime,
                            StartPlanTime = a.StartPlanTime,
                            Status = a.Status,
                            UseArea = a.UseArea,
                            UserName = a.UserName,
                            ApplyType = c.ApplyType,
                            CheckReply = c.CheckReply,
                            CheckUserName = c.CheckUserName,
                            CheckUserId = c.CheckUserId,
                            CreatDate = c.CreateDate,
                            UserCount = a.UserCount,
                            DepartmentId = a.DepartmentId,
                            FileName = a.FileName,
                            LastUpdateTime = c.LastUpdateTime,
                            UserTitle = a.UserTitle,
                            CarType = a.CarType,
                            CarProperty=a.CarProperty,
                            CheckStatus = c.CheckStatus,
                            DepartmentName = d.DepartmentName,
                            Destination = a.Destination,
                            StartPoint = a.StartPoint,
                        };

            output.Datas= new List<VehicleApplyCheckData> { await query?.FirstOrDefaultAsync()};
            return output;
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<OutputDto> CheckAsync(Guid checkId, CheckContentPostData value)
        {
            var check = await _checkRepository.Entities.Where(v => v.Id.Equals(checkId)).FirstOrDefaultAsync();
            if (check == null)
            {
                //如果没有则插入一条
                if (value.ApplyType == ApplyType.VehicleApply)
                {
                    var apply = await _applyRepository.Entities.Where(v => v.ApplyNum == value.ApplyNum).Select(v => new { v.Id }).FirstOrDefaultAsync();
                    if (apply == null)
                        throw new OneZeroException("数据异常，审核失败", ResponseCode.UnExpectedException);
                    await _checkRepository.AddAsync(new CheckContents()
                    {
                        Id = GuidHelper.NewGuid(),
                        ApplyId = apply.Id,
                        ApplyNum = value.ApplyNum,
                        ApplyType = ApplyType.VehicleApply,
                        CheckStatus = value.CheckStatus,
                        CheckUserId = value.CheckUserId,
                        CheckUserName = value.CheckUserName,
                        CheckReply = value.CheckReply,
                        CreateDate = DateTime.Now,
                        LastUpdateTime = DateTime.Now
                    });
                }

            }
            else
            {
                //更新审核状态
                check.CheckStatus = value.CheckStatus;
                check.CheckUserId = value.CheckUserId;
                check.CheckUserName = value.CheckUserName;
                check.CheckReply = value.CheckReply;
                check.LastUpdateTime = DateTime.Now;
                await _checkRepository.UpdateAsync(check);
            }
            output.Message = "操作完成";
            return output;
        }
    }
}
