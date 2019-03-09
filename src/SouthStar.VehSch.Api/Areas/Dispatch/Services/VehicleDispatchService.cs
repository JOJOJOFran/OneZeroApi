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
using SouthStar.VehSch.Api.Areas.Dispatch.Dtos;
using SouthStar.VehSch.Api.Areas.Dispatch.Models;
using SouthStar.VehSch.Api.Areas.Setting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Dispatch.Services
{
    public class VehicleDispatchService:BaseService
    {
        private ILogger<VehicleDispatchService> _logger;
        private readonly OutputDto output = new OutputDto();
        private IRepository<VehicleApplications, Guid> _applyRepository;
        private IRepository<CheckContents, Guid> _checkRepository;
        private IRepository<VehicleDispatchs, Guid> _dispatchRepository;
        private IRepository<Departments, Guid> _departmentRepository;

        public VehicleDispatchService(IUnitOfWork unitOfWork, ILogger<VehicleDispatchService> logger, IDapperProvider dapper, IMapper mapper) : base(unitOfWork, dapper, mapper)
        {
            _applyRepository = unitOfWork.Repository<VehicleApplications, Guid>();
            _checkRepository = unitOfWork.Repository<CheckContents, Guid>();
            _departmentRepository = unitOfWork.Repository<Departments, Guid>();
            _dispatchRepository= unitOfWork.Repository<VehicleDispatchs, Guid>();
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
        public async Task<OutputDto> GetVehicleDispatchListAsync(ApplyState? status, string applyNum, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int limit = 20)
        {
            int skipCount = 0;
            //找到申请状态为已审核且审核状态为通过或者已派车的申请单
            var apply = from a in _applyRepository.Entities.Where(v => (string.IsNullOrWhiteSpace(applyNum) || EF.Functions.Like(v.ApplyNum, "%" + applyNum + "%")
                                                                    && (status == null || v.Status.Equals(status))
                                                                    && (v.Status == ApplyState.Checked)                           //不能是起草的申请
                                                                    && (startDate == null || v.CreateDate >= startDate)
                                                                    && (endDate == null || v.CreateDate <= endDate))).OrderBy(v => v.ApplyNum)
                        join b in _checkRepository.Entities.Where(v => v.CheckStatus == CheckStatus.Approved || v.CheckStatus == CheckStatus.Dispatched) on a.Id equals b.ApplyId
                        select new {a.Id };

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
                        join r in _checkRepository.Entities.Where(v=>v.CheckStatus==CheckStatus.Approved|| v.CheckStatus == CheckStatus.Dispatched) on a.Id equals r.ApplyId
                        select new VehicleDispatchListData()
                        {
                            Id = dsi.Id==default(Guid)?(Guid?)null: dsi.Id,
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
    }
}
