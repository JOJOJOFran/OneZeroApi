using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using OneZero.Common.Dapper;
using OneZero.Common.Dtos;
using OneZero.Domain.Repositories;
using SouthStar.VehSch.Api.Areas.Dispatch.Models;
using SouthStar.VehSch.Api.Areas.Setting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SouthStar.VehSch.Api.Areas.Dispatch.Dtos;
using OneZero.Common.Enums;
using OneZero.Common.Exceptions;
using OneZero.Application;

namespace SouthStar.VehSch.Api.Areas.Dispatch.Services
{
    public class DispatchQueueService : BaseService
    {
        private readonly ILogger<DispatchQueueService> _logger;
        private readonly OutputDto output = new OutputDto();
        private readonly IRepository<DispatchQueue, Guid> _queueRepository;
        private readonly IRepository<Drivers, Guid> _driverRepository;
        private readonly IRepository<Vehicles, Guid> _vehicleRepository;
        private readonly IDispatchQueue _dispatchQueue;


        public DispatchQueueService(IUnitOfWork unitOfWork, ILogger<DispatchQueueService> logger, IDapperProvider dapper, IMapper mapper, IDispatchQueue dispatchQueue) : base(unitOfWork, dapper, mapper)
        {
            _queueRepository = unitOfWork.Repository<DispatchQueue, Guid>();
            _driverRepository = unitOfWork.Repository<Drivers, Guid>();
            _vehicleRepository = unitOfWork.Repository<Vehicles, Guid>();
            _logger = logger;
            _dispatchQueue = dispatchQueue;
        }

        /// <summary>
        /// 获取费用列表
        /// </summary>
        /// <param name="applyNum"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<OutputDto> GetQueueListAsync(int page = 1, int limit = 50)
        {
            int skipCount = 0;


            var queue = from a in _driverRepository.Entities.Where(v => v.Id.Equals(default(Guid)))
                        join b in _queueRepository.Entities.OrderBy(v => v.QueueNo) on a.Id equals b.DriverId into b_join
                        from bi in b_join.DefaultIfEmpty()
                        select new { a.Id };


            var sumCount = await queue.CountAsync();
            if (sumCount <= 0)
                return output;

            output.PageInfo = Paging(page, limit, sumCount, out skipCount);
            if (skipCount < 0 || output.PageInfo == null)
                return output;

            var query = from a in queue
                        join b in _driverRepository.Entities on a.Id equals b.Id
                        join c in _queueRepository.Entities on b.Id equals c.DriverId into c_join
                        from ci in c_join.DefaultIfEmpty()
                        join d in _vehicleRepository.Entities on ci.VehicleId equals d.Id into d_join
                        from di in d_join.DefaultIfEmpty()
                        select new DispatchQueueListData {
                            Id=ci.Id,
                            DriverId=a.Id,
                            DriverName=b.Name,
                            MobileNum=b.MobileNum,
                            PlateNum=di.PlateNumber,
                            QueueNo=ci.QueueNo,
                            QueueType=ci.QueueType,
                            Status=ci.Status,
                            VehicleId=di.Id,
                            VehicleProperty=di.VehicleProperties,
                            VehicleType=di.VechileType,
                            WorkCount=ci.WorkCount
                        };
            output.Datas = await query?.OrderBy(v=>v.QueueNo).OrderBy(v=>v.DriverName).ToListAsync();
            return output;
        }

        public async Task<OutputDto> BuildQueue(List<DispatchQueueData> list)
        {
            if (list == null || list.Count <= 0)
                throw new OneZeroException("创建建队列失败：参数不合法", ResponseCode.ExpectedException);
            
            return output;
        }
    }
}
