using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Common.Dapper;
using OneZero.Common.Dtos;
using OneZero.Common.Exceptions;
using OneZero.Common.Extensions;
using OneZero.Domain.Repositories;
using OneZero.Application;
using SouthStar.VehSch.Api.Areas.Setting.Dtos;
using SouthStar.VehSch.Api.Areas.Setting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Setting.Services
{
    public class DepartmentService : BaseService
    {
        private IRepository<Departments, Guid> _departmentRepository;
        private ILogger<VehcileService> _logger;
        private readonly OutputDto output = new OutputDto();

        public DepartmentService(IUnitOfWork unitOfWork, ILogger<VehcileService> logger, IDapperProvider dapper, IMapper mapper) : base(unitOfWork, dapper, mapper)
        {
            _departmentRepository = unitOfWork.Repository<Departments, Guid>();
            _logger = logger;
        }

        /// <summary>
        /// 查询车辆列表
        /// </summary>
        /// <param name="plateNumber">车牌号</param>
        /// <param name="currentState">车辆状态</param>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        public async Task<OutputDto> GetListAsync(string name = null,  int page = 1, int limit = 20)
        {
            int skipCount = 0;

            var departments = _departmentRepository.Entities.Where(v => (string.IsNullOrEmpty(name) || v.DepartmentName == name)).OrderBy(v => v.DepartmentName);
            var sumCount = await departments.Select(v => v.Id).CountAsync();
            if (sumCount <= 0)
                return output;

            output.PageInfo = Paging(page, limit, sumCount, out skipCount);
            if (skipCount < 0 || output.PageInfo == null)
                return output;

            var query = from v in departments.Skip(skipCount).Take(limit)
                        join d in _departmentRepository.Entities on v.Id equals d.Id 
                        join r in _departmentRepository.Entities on d.ParentDepartmentId equals r.Id into r_join
                        from ri in r_join.DefaultIfEmpty()
                        select new DepartmentListData()
                        {
                           Id=d.Id,
                           DepartmentName=d.DepartmentName,
                           ParentDepartmentId=d.ParentDepartmentId,
                           ParentDepartmentName=ri.DepartmentName,
                           Remark=d.Remark,
                           RowNo=d.RowNo
                        };
            output.Datas = await query?.ToListAsync();


            return output;
        }

        /// <summary>
        /// 获取车辆信息
        /// </summary>
        /// <param name="departmentId">车辆ID</param>
        /// <returns></returns>
        public async Task<OutputDto> GetItemAsync(Guid departmentId)
        {
            var department = await _departmentRepository.Entities.FirstOrDefaultAsync(v => v.Id.Equals(departmentId));
            if (department != null)
            {
                output.Datas = new List<DepartmentData> { ConvertToDataDto<Departments, DepartmentData>(department) };
            }            
            return output;
        }

        /// <summary>
        /// 新增车辆信息
        /// </summary>
        /// <returns></returns>
        public async Task<OutputDto> AddAsync(DepartmentData departmentData)
        {
            departmentData.NotNull("车辆信息(新增)");
            departmentData.Id = GuidHelper.NewGuid();
            return await _departmentRepository.AddAsync(departmentData,
                                                     null,
                                                     v => (ConvertToModel<DepartmentData, Departments>(departmentData)));
        }

        /// <summary>
        /// 标记删除部门（IsDelete置为1）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<OutputDto> MarkDeleteAsync(Guid Id)
        {
            return await _departmentRepository.MarkDeleteAsync(Id);
        }

        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="departmentInfo"></param>
        /// <returns></returns>
        public async Task<OutputDto> UpdateAsync(Guid departmentId, Departments departmentInfo)
        {
            departmentInfo.Id = departmentId;
            return await _departmentRepository.UpdateAsync(departmentInfo);
        }
    }
}
