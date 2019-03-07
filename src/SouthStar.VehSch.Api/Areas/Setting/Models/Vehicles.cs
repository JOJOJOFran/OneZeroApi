using OneZero.Application.Models;
using SouthStar.VehSch.Api.Areas.Setting.Models.Enum;
using SouthStar.VehSch.Api.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Setting.Models
{
    /// <summary>
    /// 车辆
    /// </summary>
    public class Vehicles :BaseEntity<Guid>
    {
        #region
        /// <summary>
        /// 驾驶员Id
        /// </summary>
        public Guid DriverId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string PlateNumber { get; set; }

        /// <summary>
        /// 车辆颜色
        /// </summary>
        [MaxLength(20)]
        public string VehicleColor { get; set; }

        /// <summary>
        /// 车辆品牌
        /// </summary>
        [MaxLength(50)]
        public string VehicleBrand { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string VechileType { get; set; }

        /// <summary>
        /// 核定座位数
        /// </summary>
        [Required]
        public int ApprovedSeating { get; set; }

        /// <summary>
        /// VIN(车架号)
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string VIN { get; set; }

        /// <summary>
        /// 发动机号
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string EngineNo { get; set; }

        /// <summary>
        /// 购入价格
        /// </summary>
        public int PurchasePrice { get; set; }

        /// <summary>
        /// GPS终端编号
        /// </summary>
        [MaxLength(30)]
        public string TerminalNo { get; set; }

        /// <summary>
        /// 初始里程
        /// </summary>
        public int InitMileage { get; set; }

        /// <summary>
        /// 油箱容量
        /// </summary>
        public int TankCapacity { get; set; }

        /// <summary>
        /// 行驶证拥有者
        /// </summary>
        [MaxLength(30)]
        public string VehicleLicOwner { get; set; }

        /// <summary>
        /// 账面原值
        /// </summary>
        [MaxLength(20)]
        public string BookOriginValue { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        [MaxLength(20)]
        public string Displacament { get; set; }

        /// <summary>
        /// 车辆性质
        /// </summary>
        [Required]
        public CarProperty VehicleProperties { get; set; }

        /// <summary>
        /// 固定资产卡编号
        /// </summary>
        [MaxLength(20)]
        public string FACardNum { get; set; }

        /// <summary>
        /// 油料类型
        /// </summary>
        [MaxLength(20)]
        public OilType OilType { get; set; }

        /// <summary>
        /// 车辆图标
        /// </summary>
        [MaxLength(100)]
        public string CarIcon { get; set; }

        /// <summary>
        /// 载重质量
        /// </summary>
        [MaxLength(20)]
        public string LoadWeight { get; set; }

        /// <summary>
        /// 整备质量
        /// </summary>
        [MaxLength(20)]
        public string CurbWeight { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }

        /// <summary>
        /// 车辆状态xq
        /// </summary>
        [MaxLength(50)]
        public CurrentState CurrentState { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [MaxLength(100)]
        public string Image { get; set; }
        #endregion


    }
}
