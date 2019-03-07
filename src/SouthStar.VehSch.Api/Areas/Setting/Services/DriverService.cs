using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Common.Dapper;
using OneZero.Common.Dtos;
using OneZero.Common.Exceptions;
using OneZero.Common.Extensions;
using OneZero.Domain.Repositories;
using SouthStar.VehSch.Api.Areas.Setting.Dtos;
using SouthStar.VehSch.Api.Areas.Setting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Setting.Services
{
    public class DriverService:BaseService
    {
        private IRepository<Vehicles, Guid> _vehicleRepository;
        private IRepository<Departments, Guid> _departmentRepository;
        private IRepository<Drivers, Guid> _driverRepository;
        private ILogger<DriverService> _logger;
        private readonly OutputDto output = new OutputDto();

        public DriverService(IUnitOfWork unitOfWork, ILogger<DriverService> logger, IDapperProvider dapper, IMapper mapper) : base(unitOfWork, dapper, mapper)
        {
            _vehicleRepository = unitOfWork.GetRepository<Vehicles, Guid>();
            _departmentRepository = unitOfWork.GetRepository<Departments, Guid>();
            _driverRepository = unitOfWork.GetRepository<Drivers, Guid>();
            _logger = logger;
        }

        /// <summary>
        /// 查询司机列表
        /// </summary>
        /// <param name="plateNumber">车牌号</param>
        /// <param name="currentState">车辆状态</param>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        public async Task<OutputDto> GetListAsync(string name = null, string drivingLicNum = null, Guid? departmentId = null, int page = 1, int limit = 20)
        {
            int skipCount = 0;
            var drivers = _driverRepository.Entities.Where(v => (string.IsNullOrEmpty(drivingLicNum) || v.DrivingLicNum == drivingLicNum)
                                                                     && (name == null || v.Name == name)
                                                                     && (departmentId == null || v.DepartmentId.Equals(departmentId))).OrderBy(v => v.Name);
            var sumCount = await drivers.Select(v => v.Id).CountAsync();
            if (sumCount <= 0)
                return output;

            output.PageInfo = Paging(page, limit, sumCount, out skipCount);
            if (skipCount < 0 || output.PageInfo == null)
                return output;

            var query = from item in drivers.Skip(skipCount).Take(limit)
                        join d in _departmentRepository.Entities on item.DepartmentId equals d.Id into d_join
                        from di in d_join.DefaultIfEmpty()                      
                        select new DriverListData()
                        {
                            Address = item.Address,
                            DrivingLicNum = item.DrivingLicNum,
                            DepartmentId = item.DepartmentId,
                            DepartmentName = string.IsNullOrEmpty(di.DepartmentName) ? "-" : di.DepartmentName,
                            Id = item.Id,
                            ExpirationDate = item.ExpirationDate,
                            IssueDate = item.IssueDate,
                            MobileNum = item.MobileNum,
                            Name = item.Name,
                            PermittedType = item.PermittedType,
                            PhoneNum = item.PhoneNum,
                            Remark = item.Remark,
                            Sex = item.Sex.GetRemark(),
                            Status = item.Status.GetRemark(),
                        };
            output.Datas = await query?.ToListAsync();


            return output;
        }

        /// <summary>
        /// 获取司机信息
        /// </summary>
        /// <param name="VehicleId">司机ID</param>
        /// <returns></returns>
        public async Task<OutputDto> GetItemAsync(Guid driverId)
        {
            var driver = await _driverRepository.Entities.FirstOrDefaultAsync(v => v.Id.Equals(driverId));
            output.Datas = new List<DriverData> { ConvertToDataDto<Drivers, DriverData>(driver) };
            return output;
        }


        /// <summary>
        /// 新增司机信息
        /// </summary>
        /// <returns></returns>
        public async Task<OutputDto> AddAsync(DriverData driverData)
        {
            driverData.NotNull("司机信息(新增)");
            var driver = _mapper.Map<Drivers>(driverData);
            driver.Id = GuidHelper.NewGuid();
            return await _driverRepository.AddAsync(driverData,
                                                     null,
                                                     v => (ConvertToModel<DriverData, Drivers>(driverData)));
        }

        /// <summary>
        /// 标记删除司机（IsDelete置为1）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<OutputDto> MarkDeleteAsync(Guid Id)
        {
            return await _driverRepository.MarkDeleteAsync(Id);
        }

        /// <summary>
        /// 删除司机（从数据表中删除）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<OutputDto> DeleteAsync(Guid Id)
        {
            return await _driverRepository.DeleteAsync(Id);
        }

        /// <summary>
        /// 更新司机信息
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="vehicleInfo"></param>
        /// <returns></returns>
        public async Task<OutputDto> UpdateAsync(Guid driverId, Drivers driverInfo)
        {
            driverInfo.Id = driverId;
            return await _driverRepository.UpdateAsync(driverInfo);
        }
    }
}
