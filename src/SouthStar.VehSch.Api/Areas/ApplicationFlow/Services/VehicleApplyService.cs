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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.ApplicationFlow.Services
{
    public class VehicleApplyService : BaseService
    {
        private ILogger<VehicleApplyService> _logger;
        private readonly OutputDto output = new OutputDto();
        private IRepository<VehicleApplications, Guid> _applyRepository;
        private IRepository<CheckContents, Guid> _checkRepository;

        public VehicleApplyService(IUnitOfWork unitOfWork, ILogger<VehicleApplyService> logger, IDapperProvider dapper, IMapper mapper) : base(unitOfWork, dapper, mapper)
        {
            _applyRepository = unitOfWork.GetRepository<VehicleApplications, Guid>();
            _checkRepository = unitOfWork.GetRepository<CheckContents, Guid>();
            _logger = logger;
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
        /// <param name="applyinfo"></param>
        /// <returns></returns>
        public async Task<OutputDto> AddNotSendAsync(VehicleApplicationData applyinfo)
        {
            applyinfo.NotNull("用车申请(新增)");
            applyinfo.Id = GuidHelper.NewGuid();
            //申请状态设置为起草
            applyinfo.Status = ApplyState.Draft;
            return await _applyRepository.AddAsync(applyinfo,
                                                     null,
                                                     v => (ConvertToModel<VehicleApplicationData, VehicleApplications>(applyinfo)));
        }

        /// <summary>
        /// 新增并送审
        /// </summary>
        /// <param name="applyinfo"></param>
        /// <returns></returns>
        public async Task<OutputDto> AddAndSendAsync(VehicleApplicationData applyinfo)
        {
            try
            {
                await _unitOfWork.BeginTransAsync();
                applyinfo.NotNull("用车申请(新增)");
                applyinfo.Id = GuidHelper.NewGuid();
                //申请状态设置为待审核
                applyinfo.Status = ApplyState.WaitCheck;
                await _applyRepository.AddAsync(applyinfo,
                                                null,
                                                v => (ConvertToModel<VehicleApplicationData, VehicleApplications>(applyinfo)));

                await _checkRepository.AddAsync(new CheckContents(){Id = GuidHelper.NewGuid(),
                                                                    ApplyId = applyinfo.Id,
                                                                    ApplyNum = applyinfo.ApplyNum,
                                                                    ApplyType = ApplyType.VehicleApply,
                                                                    CheckStatus = CheckStatus.WaitCheck,
                                                                    CheckReply = "",
                                                                    CreateDate = DateTime.Now,
                                                                    LastUpdateTime = DateTime.Now});
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
        public async Task<OutputDto> UpdateAsync(Guid applyId, VehicleApplications driverInfo)
        {
            driverInfo.Id = applyId;
            return await _applyRepository.UpdateAsync(driverInfo);
        }


        /// <summary>
        /// 生成流水号
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetNextSeq()
        {
            var apply = await _applyRepository.Entities.OrderByDescending(v => v.ApplyNum).FirstOrDefaultAsync();
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
