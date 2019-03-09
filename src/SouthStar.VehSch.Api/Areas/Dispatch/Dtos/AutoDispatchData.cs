using OneZero.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Dispatch.Dtos
{
    public class AutoDispatchData:DataDto
    {
        public Guid DriverId { get; set; }

        public string DriverName { get; set; }

        public Guid? VehicleId { get; set; }
        
        public  string PlateNumber { get; set; }
    }
}
