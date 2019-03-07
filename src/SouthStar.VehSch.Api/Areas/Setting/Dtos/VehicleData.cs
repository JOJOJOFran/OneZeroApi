using OneZero.Common.Dtos;
using SouthStar.VehSch.Api.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Setting.Dtos
{
    public class VehicleData:DataDto
    {
        #region
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 驾驶员Id
        /// </summary>
        public Guid DriverId { get; set; }


        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }


        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 车牌颜色
        /// </summary>
        public string VehicleColor { get; set; }

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
        /// 发动机号
        /// </summary>
        public string EngineNo { get; set; }

        /// <summary>
        /// 购入价格
        /// </summary>
        public int PurchasePrice { get; set; }


        /// <summary>
        /// GPS终端编号
        /// </summary>       
        public string TerminalNo { get; set; }

        /// <summary>
        /// 初始里程
        /// </summary>
        public int InitMileage { get; set; }

        /// <summary>
        /// 邮箱容量
        /// </summary>
        public int TankCapacity { get; set; }

        /// <summary>
        /// 行驶证拥有者
        /// </summary>
        public string VehicleLicOwner { get; set; }

        /// <summary>
        /// 账面原值
        /// </summary>
        public string BookOriginValue { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public string Displacament { get; set; }

        /// <summary>
        /// 车辆性质
        /// </summary>
        public CarProperty VehicleProperties { get; set; }

        /// <summary>
        /// 固定资产卡编号
        /// </summary>
        public string FACardNum { get; set; }

        /// <summary>
        /// 油料类型
        /// </summary>
        public string OilType { get; set; }

        /// <summary>
        /// 车辆图标
        /// </summary>
        public string CarIcon { get; set; }

        /// <summary>
        /// 载重质量
        /// </summary>
        public string LoadWeight { get; set; }

        /// <summary>
        /// 整备质量
        /// </summary>
        public string CurbWeight { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Image { get; set; }
        #endregion
    }
}
