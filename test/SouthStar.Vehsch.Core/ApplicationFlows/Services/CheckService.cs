using AutoMapper;
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

using SouthStar.VehSch.Core.Setting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SouthStar.VehSch.Core.Common.Enums;
using OneZero.Common.Helpers;

namespace SouthStar.VehSch.Core.ApplicationFlow.Services
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

            var apply = _applyRepository.Entities.Where(v => (string.IsNullOrWhiteSpace(applyNum) || EF.Functions.Like(v.ApplyNum, "%" + applyNum + "%"))
                                                                    && (status == null || v.Status.Equals(status))
                                                                    && (v.Status != ApplyState.Draft)                           //不能是起草的申请
                                                                    && (startDate == null || v.CreateDate >= startDate)
                                                                    && (endDate == null || v.CreateDate <= endDate)).OrderBy(v => v.ApplyNum);
            var sumCount = await apply.Select(v => new { v.Id }).CountAsync();
            if (sumCount <= 0)
                return output;

            output.PageInfo = Paging(page, limit, sumCount, out skipCount);
            if (skipCount < 0 || output.PageInfo == null)
                return output;

            var query = from a in apply.Skip(skipCount).Take(limit)
                        join d in _departmentRepository.Entities on a.DepartmentId equals d.Id into d_join
                        from di in d_join.DefaultIfEmpty()
                        join r in _checkRepository.Entities on a.Id equals r.ApplyId into r_join
                        from ri in r_join.DefaultIfEmpty()
                        select new VehicleApplicationListData()
                        {
                            ApplicantName = a.ApplicantName,
                            Id = a.Id,
                            ApplyNum = a.ApplyNum,
                            ApplyReson = a.ApplyReson,
                            ApplicantPhone = a.ApplicantPhone,
                            BackPlanTime = a.BackPlanTime,
                            StartPlanTime = a.StartPlanTime,
                            Status = a.Status.GetRemark(),
                            UseArea = a.UseArea,
                            UserName = a.UserName,
                            UserMobile = a.UserMobile,
                            CarType = a.CarType,
                            CarProperty = a.CarProperty.GetRemark(),
                            CheckStatus = ri.CheckStatus.GetRemark(),
                            DepartmentName = String.IsNullOrEmpty(di.DepartmentName) ? "-" : di.DepartmentName,
                            Destination = a.Destination,
                            StartPoint = a.StartPoint,
                        };
            output.Datas = await query?.ToListAsync();
            return output;
        }


        public async Task<OutputDto> GetApplyWithCheckContent(Guid id)
        {
            var query = from a in _applyRepository.Entities.Where(v=>v.Id.Equals(id))
                        join d in _departmentRepository.Entities on a.DepartmentId equals d.Id into d_join
                        from di in d_join.DefaultIfEmpty()
                        join r in _checkRepository.Entities on a.Id equals r.ApplyId into r_join
                        from ri in r_join.DefaultIfEmpty()
                        select new
                        {
                            ApplicantName = a.ApplicantName,
                            Id = a.Id,
                            ApplyNum = a.ApplyNum,
                            ApplyReson = a.ApplyReson,
                            ApplicantPhone = a.ApplicantPhone,
                            BackPlanTime = a.BackPlanTime,
                            StartPlanTime = a.StartPlanTime,
                            Status = a.Status.GetRemark(),
                            UseArea = a.UseArea,
                            UserName = a.UserName,
                            UserMobile = a.UserMobile,
                            CarType = a.CarType,
                            CarProperty = a.CarProperty.GetRemark(),
                            CheckStatus = ri.CheckStatus.GetRemark(),
                            DepartmentName = String.IsNullOrEmpty(di.DepartmentName) ? "-" : di.DepartmentName,
                            Destination = a.Destination,
                            StartPoint = a.StartPoint,
                            CheckId=ri.Id,
                            ri.CheckUserId,
                            ri.CheckUserName,
                            ri.CheckReply,
                            CheckDate=ri.CreateDate
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
            var msg = "";
            if (!CheckContentValidate(value, out msg))
            {
                output.Message = msg;
                return output;
            }

            var check = await _checkRepository.Entities.Where(v => v.Id.Equals(checkId)).FirstOrDefaultAsync();
            if (check == null)
            {
                output.Message = "未找到审核单";
                return output;
            }

            try
            {
                //开启事务
                await _unitOfWork.BeginTransAsync();
                //针对申请类型，做相应处理
                switch (value.ApplyType)
                {
                    case ApplyType.VehicleApply:
                        await VehicleApplyCheck(value);
                        break;
                }
                //更新审核状态
                check.CheckStatus = value.CheckStatus;
                check.CheckUserId = value.CheckUserId;
                check.CheckUserName = value.CheckUserName;
                check.CheckReply = value.CheckReply;
                check.LastUpdateTime = DateTime.Now;
                await _checkRepository.UpdateAsync(check);
                await _unitOfWork.CommitAsync();
                output.Message = "审核成功";
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                if (e is OneZeroException)
                {
                    throw e;
                }
                throw new OneZeroException($"{value?.ApplyType.GetRemark()}审核失败",e,ResponseCode.UnExpectedException);
            }        
            return output;
        }

        private async Task VehicleApplyCheck(CheckContentPostData value)
        {
            var apply = await _applyRepository.Entities.FirstOrDefaultAsync(v=>v.ApplyNum==value.ApplyNum);
            if (apply == null)
            {
                throw new OneZeroException("未找到申请单", ResponseCode.ExpectedException);
            }

            apply.Status = ApplyState.Checked;
            await _applyRepository.UpdateOneAsync(apply);
        }


        /// <summary>
        /// 审核内容校验
        /// </summary>
        /// <param name="value"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool CheckContentValidate(CheckContentPostData value, out string msg)
        {
            msg = "";
            bool checkFlag = true;
            if (string.IsNullOrWhiteSpace(value.ApplyNum))
            {
                msg = "申请编号不能为空";
                checkFlag = false;
            }

            return checkFlag;
        }
    }
}
