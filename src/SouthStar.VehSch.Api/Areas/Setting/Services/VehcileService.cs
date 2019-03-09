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
            #region sql执行示例：
            //string sql = $"SELECT V.Id, V.VIN, V.VehicleProperties, V.VehicleBrand, V.VechileType, V.TerminalNo, V.Remark, V.PlateNumber, D.DepartmentName, V.CurrentState, V.ApprovedSeating, R.Name AS DriverName " +
            //    $" FROM dbo.TVehicles V FROM dbo.TVehicles V " +
            //    $" LEFT JOIN dbo.TDepartments D ON V.DepartmentId=D.Id " +
            //    $" LEFT JOIN dbo.TDrivers R ON V.DriverId=R.Id " +
            //    $" WHERE 1=1 ";
            //if (!string.IsNullOrWhiteSpace(plateNumber))
            //    sql += $" AND V.PlateNumber LIKE '%'+'{plateNumber}'+'%' ";
            //if (!string.IsNullOrWhiteSpace(currentState))
            //    sql += $" AND V.CurrentState='{currentState}' ";
            //if (departmentId != null)
            //    sql += $"  AND D.Id='{departmentId}' ";

            //IEnumerable<VehicleListData> data = (IEnumerable<VehicleListData>)await _dapper.FromSqlAsync(DefaultConnectString, sql);
            //output.Datas = data;
            #endregion
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
        /// 获取车辆信息
        /// </summary>
        /// <param name="VehicleId">车辆ID</param>
        /// <returns></returns>
        public async Task<OutputDto> GetItemAsync(Guid VehicleId)
        {
            var vehicle = await _vehicleRepository.Entities.FirstOrDefaultAsync(v => v.Id.Equals(VehicleId));
            output.Datas = new List<VehicleData> { ConvertToDataDto<Vehicles, VehicleData>(vehicle) };
            return output;
        }


        /// <summary>
        /// 新增车辆信息
        /// </summary>
        /// <returns></returns>
        public async Task<OutputDto> AddAsync(VehicleData vehicleData)
        {
            vehicleData.NotNull("车辆信息(新增)");
            vehicleData.Id = GuidHelper.NewGuid();
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
        public async Task<OutputDto> UpdateAsync(Guid vehicleId, Vehicles vehicleInfo)
        {
            vehicleInfo.Id = vehicleId;
            return await _vehicleRepository.UpdateAsync(vehicleInfo);
        }

    }
}
