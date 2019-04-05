using AutoMapper;
using OneZero.Core.Dtos.Permission;
using OneZero.Core.Models.Permissions;
using SouthStar.VehSch.Core.ApplicationFlow.Dtos;
using SouthStar.VehSch.Core.ApplicationFlow.Models;
using SouthStar.VehSch.Core.Dispatch.Dtos;
using SouthStar.VehSch.Core.Dispatch.Models;
using SouthStar.VehSch.Core.Setting.Dtos;
using SouthStar.VehSch.Core.Setting.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouthStar.VehSch.Core.Common
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

            CreateMap<VehicleApplications, VehicleApplicationData>();
            CreateMap<VehicleApplicationData, VehicleApplications>();

            CreateMap<CheckContentData, CheckContents>();
            CreateMap<CheckContents, CheckContentData>();

            CreateMap<VehicleDispatchs, VehicleDispatchData>();
            CreateMap<VehicleDispatchData, VehicleDispatchs>();

            CreateMap<DispatchFeeData, DispatchFees>();
            CreateMap<DispatchFees, DispatchFeeData>();

            CreateMap<UserData, User>().ForMember(d => d.PasswordHash, opt => opt.MapFrom(s => s.Password));
        }
    }
}
