using AutoMapper;
using SouthStar.VehSch.Api.Areas.Setting.Dtos;
using SouthStar.VehSch.Api.Areas.Setting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Common.AutoMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<VehicleData, Vehicles>();
            CreateMap<Vehicles, VehicleData>();
            CreateMap<DriverData, Drivers>();
            CreateMap<Drivers, DriverData>();
            CreateMap<DepartmentData, Departments>();
            CreateMap<Departments, DepartmentData>();

            CreateMap<DepartmentData, Departments>();
        }
    }
}
