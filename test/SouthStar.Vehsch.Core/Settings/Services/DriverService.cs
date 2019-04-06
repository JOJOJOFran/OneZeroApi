using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneZero.Core;
using OneZero.Common.Dapper;
using OneZero.Dtos;
using OneZero.Exceptions;
using OneZero.Common.Extensions;
using OneZero.Domain;
using SouthStar.VehSch.Core.Setting.Dtos;
using SouthStar.VehSch.Core.Setting.Models;
using SouthStar.VehSch.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneZero.Common.Helpers;

namespace SouthStar.VehSch.Core.Setting.Services
{
    public class DriverService : BaseService
    {
        private readonly IRepository<Vehicles, Guid> _vehicleRepository;
        private readonly IRepository<Departments, Guid> _departmentRepository;
        private readonly IRepository<Drivers, Guid> _driverRepository;

        private readonly ILogger<DriverService> _logger;
        private readonly OutputDto output = new OutputDto();

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        /// <param name="dapper"></param>
        /// <param name="mapper"></param>
        public DriverService(IUnitOfWork unitOfWork,
                             ILogger<DriverService> logger,
                             IDapperProvider dapper,
                             IMapper mapper) : base(unitOfWork, dapper, mapper)
        {
            _vehicleRepository = unitOfWork.Repository<Vehicles, Guid>();
            _departmentRepository = unitOfWork.Repository<Departments, Guid>();
            _driverRepository = unitOfWork.Repository<Drivers, Guid>();
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
            var drivers = _driverRepository.Entities.Where(v => (string.IsNullOrEmpty(drivingLicNum) || EF.Functions.Like(v.DrivingLicNum, "%" + drivingLicNum + "%"))
                                                                     && (name == null ||  EF.Functions.Like(v.Name,$"%{name}%"))
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
        /// 获取可用司机
        /// </summary>
        /// <returns></returns>
        public async Task<OutputDto> GetEnableList()
        {
            var query = _driverRepository.Entities.Where(v => v.Status == PersonState.OnWait || v.Status == PersonState.OnDuty)
                                                  .OrderBy(v => v.Name)
                                                  .Select(v => new { v.Id, Desc=v.Name+$"({v.MobileNum})",v.Name ,v.MobileNum, v.Status, v.PermittedType });
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
        /// <param name="driverId"></param>
        /// <param name="driverData"></param>
        /// <returns></returns>
        public async Task<OutputDto> UpdateAsync(Guid driverId, DriverData driverData)
        {
            var driverInfo = ConvertToModel<DriverData, Drivers>(driverData);
            driverInfo.Id = driverId;
            return await _driverRepository.UpdateAsync(driverInfo);
        }

        /// <summary>
        /// 改变司机状态
        /// </summary>
        /// <param name="driverId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<OutputDto> ChangeStatusAsync(Guid driverId, PersonState state)
        {
            var driver = await _driverRepository.Entities.Where(v => v.Id.Equals(driverId)).FirstOrDefaultAsync();
            output.Message = "数据不存在";
            if (driver == null)
                return output;
            driver.Status = state;
            var result = await _driverRepository.UpdateOneAsync(driver);
            if (result == 1)
                output.Message = "操作成功";
            return output;
        }

        /// <summary>
        /// 改变司机状态事件处理
        /// </summary>
        /// <param name="oldDriverId"></param>
        /// <param name="driverId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<string> ChangeStatusHandlerAsync(Guid driverId, PersonState state)
        {

            int result = 1;

            var driver = await _driverRepository.Entities.Where(v => v.Id.Equals(driverId)).FirstOrDefaultAsync();
            if (driver == null)
                return "司机不存在";

            driver.Status = state;
            result = await _driverRepository.UpdateOneAsync(driver);


            return "操作成功";
        }


        /// <summary>
        /// 改变司机状态事件处理
        /// </summary>
        /// <param name="oldDriverId"></param>
        /// <param name="driverId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<string> ChangeStatusHandlerAsync(Guid? oldDriverId, Guid driverId, PersonState state)
        {
            Drivers oldDriver;
            int oldResult = 1;
            int result = 1;

            var driver = await _driverRepository.Entities.Where(v => v.Id.Equals(driverId)).FirstOrDefaultAsync();
            if (driver == null)
                return "司机不存在";

            if (oldDriverId.HasValue&&!oldDriverId.Equals(driverId))
            {
                oldDriver = await _driverRepository.Entities.Where(v => v.Id.Equals(oldDriverId)).FirstOrDefaultAsync();
                oldDriver.Status = PersonState.OnWait;
                oldResult = await _driverRepository.UpdateOneAsync(oldDriver);
            }

            driver.Status = state;
            result = await _driverRepository.UpdateOneAsync(driver);


            return "操作成功";
        }

        public async Task RebackDriver()
        {
            await Task.Run(() => Console.WriteLine("RebackDriver开始执行"));
        }
    }
}
