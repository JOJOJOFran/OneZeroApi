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
    public class VehicleApplyService : BaseService
    {
        private ILogger<VehicleApplyService> _logger;
        private readonly OutputDto output = new OutputDto();
        private IRepository<VehicleApplications, Guid> _applyRepository;
        private IRepository<CheckContents, Guid> _checkRepository;
        private IRepository<Departments, Guid> _departmentRepository;

        public VehicleApplyService(IUnitOfWork unitOfWork, ILogger<VehicleApplyService> logger, IDapperProvider dapper, IMapper mapper) : base(unitOfWork, dapper, mapper)
        {
            _applyRepository = unitOfWork.Repository<VehicleApplications, Guid>();
            _checkRepository = unitOfWork.Repository<CheckContents, Guid>();
            _departmentRepository = unitOfWork.Repository<Departments, Guid>();
            _logger = logger;
        }

        /// <summary>
        /// 改变审核状态
        /// </summary>
        /// <param name="applyId"></param>
        /// <returns></returns>
        public async Task<string> ChangeStatusHandlerAsync(Guid applyId)
        {

            int result = 1;
            var apply = await _checkRepository.Entities.Where(v => v.ApplyId.Equals(applyId)).FirstOrDefaultAsync();
            if (apply == null)
                return "申请单未审核或不存在";

            apply.CheckStatus = CheckStatus.Dispatched;
            result = await _checkRepository.UpdateOneAsync(apply);
            return "操作成功";
        }


        /// <summary>
        /// 查询申请列表
        /// </summary>
        /// <param name="applicantId">申请人Id</param>
        /// <param name="status">申请状态</param>
        /// <param name="applyNum">申请编号</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetListAsync(Guid? applicantId,ApplyState? status,string applyNum, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int limit = 20)
        {
            int skipCount = 0;

            var apply = _applyRepository.Entities.Where(v => (string.IsNullOrWhiteSpace(applyNum) || EF.Functions.Like(v.ApplyNum, "%" + applyNum + "%")
                                                                    && (status == null || v.Status.Equals(status))
                                                                    && (applicantId == null || v.ApplicantId.Equals(applicantId))
                                                                    &&(startDate==null|| v.CreateDate>= startDate)
                                                                    &&(endDate==null||v.CreateDate<=endDate))).OrderBy(v => v.ApplyNum);
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


        /// <summary>
        /// 获取用车申请信息
        /// </summary>
        /// <param name="applyId">用车申请ID</param>
        /// <returns></returns>
        public async Task<OutputDto> GetItemAsync(Guid applyId)
        {
            var driver = await _applyRepository.Entities.FirstOrDefaultAsync(v => v.Id.Equals(applyId));
            output.Datas = new List<VehicleApplicationData> { ConvertToDataDto<VehicleApplications, VehicleApplicationData>(driver) };
            return output;
        }



        /// <summary>
        /// 新增不送审
        /// </summary>
        /// <param name="applyInfo"></param>
        /// <returns></returns>
        public async Task<OutputDto> AddNotSendAsync(VehicleApplicationData applyInfo)
        {
            applyInfo.NotNull("用车申请(新增)");
            applyInfo.Id = GuidHelper.NewGuid();
            applyInfo.ApplyNum = await GetNextSeq();
            //申请状态设置为起草
            applyInfo.Status = ApplyState.Draft;
            applyInfo.CreateDate = DateTime.Now;
            return await _applyRepository.AddAsync(applyInfo,
                                                     null,
                                                     v => (ConvertToModel<VehicleApplicationData, VehicleApplications>(applyInfo)));
        }

        /// <summary>
        /// 新增并送审
        /// </summary>
        /// <param name="applyInfo"></param>
        /// <returns></returns>
        public async Task<OutputDto> AddAndSendAsync(VehicleApplicationData applyInfo)
        {
            try
            {
                await _unitOfWork.BeginTransAsync();
                applyInfo.NotNull("用车申请(新增)");
                applyInfo.Id = GuidHelper.NewGuid();
                applyInfo.ApplyNum = await GetNextSeq();
                //申请状态设置为待审核
                applyInfo.Status = ApplyState.WaitCheck;
                applyInfo.CreateDate = DateTime.Now;
                await _applyRepository.AddAsync(applyInfo,
                                                null,
                                                v => (ConvertToModel<VehicleApplicationData, VehicleApplications>(applyInfo)));

                var result =await CreateCheckContent((Guid)applyInfo.Id, applyInfo.ApplyNum);
                //已经存在了申请id或者申请编号一致的审核内容，需重新发起
                if (result ==0)
                {                    
                    throw new OneZeroException($"送审失败:请重试");
                }
                await _unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                throw new OneZeroException("发起申请失败", e, ResponseCode.UnExpectedException);
            }
            output.Message = "申请已发出";
            return output;
        }

        /// <summary>
        /// 标记删除用车申请（IsDelete置为1）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<OutputDto> MarkDeleteAsync(Guid Id)
        {
            return await _applyRepository.MarkDeleteAsync(Id);
        }

        /// <summary>
        /// 删除用车申请（从数据表中删除）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<OutputDto> DeleteAsync(Guid Id)
        {
            return await _applyRepository.DeleteAsync(Id);
        }

        /// <summary>
        /// 更新用车申请信息
        /// </summary>
        /// <param name="applyId"></param>
        /// <param name="applyInfo"></param>
        /// <returns></returns>
        public async Task<OutputDto> UpdateAsync(Guid applyId, VehicleApplicationData applyInData)
        {
            var applyInfo = ConvertToModel<VehicleApplicationData, VehicleApplications>(applyInData);
            applyInfo.Id = applyId;
            int result = 1;
            var applyEntity =await _applyRepository.Entities.FirstOrDefaultAsync(v => v.Id.Equals(applyId));
            try
            {
                await _unitOfWork.BeginTransAsync();
                await _applyRepository.UpdateAsync(applyInfo);
                //如果之前是起草，现在是待审核，则创建审核数据
                if ((!(applyInfo.Status == applyEntity.Status)) && applyEntity.Status == ApplyState.Draft && applyInfo.Status == ApplyState.WaitCheck)
                     result=await CreateCheckContent(applyId, applyInfo.ApplyNum);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                throw new OneZeroException("发起申请失败", e, ResponseCode.UnExpectedException);
            }
            output.Message = "操作成功";
            return output;
        }


        /// <summary>
        /// 创建审核数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="applyNum"></param>
        /// <returns></returns>
        private async Task<int> CreateCheckContent(Guid id, string applyNum)
        {
            var test = await _checkRepository.Entities.ToListAsync();
            //不存在则新建
            if ((await _checkRepository.Entities.Where(v => v.ApplyId.Equals(id)|| v.ApplyNum== applyNum).Select(v => (new { v.Id })).FirstOrDefaultAsync()) == null)
            {
                return await _checkRepository.AddAsync(new CheckContents()
                {
                    Id = GuidHelper.NewGuid(),
                    ApplyId = id,
                    ApplyNum = applyNum,
                    ApplyType = ApplyType.VehicleApply,
                    CheckStatus = CheckStatus.WaitCheck,
                    CheckReply = "",
                    CreateDate = DateTime.Now,
                    LastUpdateTime = DateTime.Now
                });
            }
            else
            {
                return 0;
            }
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
