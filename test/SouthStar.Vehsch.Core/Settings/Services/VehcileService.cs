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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SouthStar.VehSch.Core.Common.Enums;
using OneZero.Common.Helpers;

namespace SouthStar.VehSch.Core.Setting.Services
{
    /// <summary>
    /// 车辆服务
    /// Lifetime:Scope
    /// </summary>
    public class VehcileService : BaseService
    {

        private IRepository<Vehicles, Guid> _vehicleRepository;
        private IRepository<Departments, Guid> _departmentRepository;
        private IRepository<Drivers, Guid> _driverRepository;
        private ILogger<VehcileService> _logger;
        private readonly OutputDto output = new OutputDto();


        public VehcileService(IUnitOfWork unitOfWork, ILogger<VehcileService> logger, IDapperProvider dapper, IMapper mapper) : base(unitOfWork, dapper, mapper)
        {
            _vehicleRepository = unitOfWork.Repository<Vehicles, Guid>();
            _departmentRepository = unitOfWork.Repository<Departments, Guid>();
            _driverRepository = unitOfWork.Repository<Drivers, Guid>();
            _logger = logger;
        }

        /// <summary>
        /// 查询车辆列表
        /// </summary>
        /// <param name="plateNumber">车牌号</param>
        /// <param name="currentState">车辆状态</param>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        public async Task<OutputDto> GetListAsync(string plateNumber = null, int? currentState = null, Guid? departmentId = null, int page=1, int limit = 20)
        {
            int skipCount = 0;

            var vehicles = _vehicleRepository.Entities.Where(v => (string.IsNullOrEmpty(plateNumber) || v.PlateNumber == plateNumber)
                                                                    && (currentState == null || v.CurrentState.Equals(1)
                                                                    && (departmentId == null || v.DepartmentId.Equals(departmentId)))).OrderBy(v => v.PlateNumber);
            var sumCount = await vehicles.Select(v => v.Id).CountAsync();
            if (sumCount <= 0)
                return output;

            output.PageInfo = Paging(page, limit, sumCount, out skipCount);
            if (skipCount < 0 || output.PageInfo == null)
                return output;

            var query = from v in vehicles.Skip(skipCount).Take(limit)
                        join d in _departmentRepository.Entities on v.DepartmentId equals d.Id into d_join
                        from di in d_join.DefaultIfEmpty()
                        join r in _driverRepository.Entities on v.DriverId equals r.Id into r_join
                        from ri in r_join.DefaultIfEmpty()
                        select new VehicleListData()
                        {
                            Id = v.Id,
                            VIN = v.VIN,
                            VehicleProperties = v.VehicleProperties==0?"公务用车":"应急执法",
                            VehicleBrand = v.VehicleBrand,
                            VechileType = v.VechileType,
                            TerminalNo = v.TerminalNo,
                            Remark = v.Remark,
                            PlateNumber = v.PlateNumber,
                            DriverId = v.DriverId,
                            DriverName = string.IsNullOrEmpty(ri.Name) ? "—" : ri.Name,
                            DepartmentId = v.DepartmentId,
                            DepartmentName = string.IsNullOrEmpty(di.DepartmentName) ? "—" : di.DepartmentName,
                            ApprovedSeating = v.ApprovedSeating,
                            CurrentState = v.CurrentState.GetRemark()
                        };
            output.Datas = await query?.ToListAsync();


            return output;
        }

        /// <summary>
        /// 获取指定性质的可用车辆
        /// </summary>
        /// <returns></returns>
        public async Task<OutputDto> GetEnableListAsync(CarProperty carProperty)
        {
            var query = _vehicleRepository.Entities.Where(v => v.CurrentState.Equals(CurrentState.OnWait)&&v.VehicleProperties.Equals(carProperty))
                                                  .OrderBy(v => v.PlateNumber)
                                                  .Select(v => new { v.Id, Desc=v.PlateNumber+$"({v.VehicleProperties.GetRemark()})",v.PlateNumber, v.VehicleProperties, v.VechileType });
            output.Datas = await query?.ToListAsync();
            return output;
        }


        /// <summary>
        /// 获取全部可用车辆
        /// </summary>
        /// <param name="carProperty"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetEnableList()
        {
            var query = _vehicleRepository.Entities.Where(v => v.CurrentState == CurrentState.OnWait )
                                                  .OrderBy(v => v.PlateNumber)
                                                  .Select(v => new { v.Id, Desc = v.PlateNumber + $"({v.VehicleProperties.GetRemark()})",v.PlateNumber, v.VehicleProperties, v.VechileType });
            output.Datas = await query?.ToListAsync();
            return output;
        }


        /// <summary>
        /// 获取车辆信息
        /// </summary>
        /// <param name="VehicleId">车辆ID</param>
        /// <returns></returns>
        public async Task<OutputDto> GetItemAsync(Guid VehicleId)
        {
            var vehicle = await _vehicleRepository.Entities.FirstOrDefaultAsync(v => v.Id.Equals(VehicleId));
            output.Datas = vehicle;
            return output;
        }


        /// <summary>
        /// 新增车辆信息
        /// </summary>
        /// <returns></returns>
        public async Task<OutputDto> AddAsync(VehicleData vehicleData)
        {
            vehicleData.NotNull("车辆信息(新增)");

            return await _vehicleRepository.AddAsync(vehicleData,
                                                     null,
                                                     v => (ConvertToModel<VehicleData, Vehicles>(vehicleData)));
        }

        /// <summary>
        /// 标记删除车辆（IsDelete置为1）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<OutputDto> MarkDeleteAsync(Guid Id)
        {
            return await _vehicleRepository.MarkDeleteAsync(Id);
        }

        /// <summary>
        /// 删除车辆（从数据表中删除）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<OutputDto> DeleteAsync(Guid Id)
        {
            return await _vehicleRepository.DeleteAsync(Id);
        }

        /// <summary>
        /// 更新车辆信息
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="vehicleInfo"></param>
        /// <returns></returns>
        public async Task<OutputDto> UpdateAsync(Guid vehicleId, VehicleData vehicleData)
        {
            var vehicleInfo = ConvertToModel<VehicleData, Vehicles>(vehicleData);
            vehicleInfo.Id = vehicleId;
            return await _vehicleRepository.UpdateAsync(vehicleInfo);
        }

        /// <summary>
        /// 改变车辆状态
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<OutputDto> ChangeStatusAsync(Guid vehicleId, CurrentState state)
        {
           var vehicle= await _vehicleRepository.Entities.Where(v => v.Id.Equals(vehicleId)).FirstOrDefaultAsync();
            output.Message = "数据不存在";
            if (vehicle == null)
                return output;

            vehicle.CurrentState = state;
            var result= await _vehicleRepository.UpdateOneAsync(vehicle);
            if(result==1)
                output.Message = "操作成功";
            return output;
        }

        /// <summary>
        /// 修改车辆状态
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<string> ChangeStatusHandlerAsync(Guid vehicleId, CurrentState state)
        {
          
            int result = 1;
            var vehicle = await _vehicleRepository.Entities.Where(v => v.Id.Equals(vehicleId)).FirstOrDefaultAsync();
            if (vehicle == null)
                return "车辆不存在";
           

            vehicle.CurrentState = state;
            result = await _vehicleRepository.UpdateOneAsync(vehicle);
            return "操作成功";

        }

        /// <summary>
        /// 修改车辆状态
        /// </summary>
        /// <param name="oldVehicleID"></param>
        /// <param name="vehicleId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<string> ChangeStatusHandlerAsync(Guid oldVehicleID, Guid vehicleId, CurrentState state)
        {
            Vehicles oldDriver;
            int oldResult = 1;
            int result = 1;
            var vehicle = await _vehicleRepository.Entities.Where(v => v.Id.Equals(vehicleId)).FirstOrDefaultAsync();
            if (vehicle == null)
                return "车辆不存在";
            if (!oldVehicleID.Equals(vehicleId))
            {
                oldDriver = await _vehicleRepository.Entities.Where(v => v.Id.Equals(oldVehicleID)).FirstOrDefaultAsync();
                oldDriver.CurrentState = CurrentState.OnWait;
                oldResult = await _vehicleRepository.UpdateOneAsync(oldDriver);
            }

            vehicle.CurrentState = state;
            result = await _vehicleRepository.UpdateOneAsync(vehicle);
            return "操作成功";

        }


    }
}
