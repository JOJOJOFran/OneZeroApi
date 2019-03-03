using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Setting.Dtos
{
    public class VehicleListData
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// 驾驶员Id
        /// </summary>
        public Guid DriverId { get; set; }


        /// <summary>
        /// 驾驶员姓名
        /// </summary>
        public string DriverName { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        // <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }


        /// <summary>
        /// 车辆品牌
        /// </summary>
        public string VehicleBrand { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public string VechileType { get; set; }

        /// <summary>
        /// 核定座位数
        /// </summary>
        public int ApprovedSeating { get; set; }

        /// <summary>
        /// VIN(车架号)
        /// </summary>
        public string VIN { get; set; }

        /// <summary>
        /// 车辆性质
        /// </summary>
        public string VehicleProperties { get; set; }

        /// <summary>
        /// 车辆当前状太
        /// </summary>
        public string CurrentState { get; set; }

        /// <summary>
        /// GPS终端编号
        /// </summary>
        public string TerminalNo { get; set; }

        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
