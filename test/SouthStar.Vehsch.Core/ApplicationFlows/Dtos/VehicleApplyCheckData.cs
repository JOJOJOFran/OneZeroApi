using Newtonsoft.Json;
using OneZero.Common.Converts;
using OneZero.Dtos;
using SouthStar.VehSch.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.ApplicationFlow.Dtos
{
    /// <summary>
    /// 用车申请审核明细数据
    /// </summary>
    public class VehicleApplyCheckData : DataDto
    {
        #region 审批
        /// <summary>
        /// 审批ID
        /// </summary>
        [JsonConverter(typeof(GuidJsonConvert))]
        public Guid Id { get; set; }

        [JsonConverter(typeof(GuidJsonConvert))]
        public Guid ApplyId  { get; set; }

        /// <summary>
        /// 申请单编号
        /// </summary>
        public string ApplyNum { get; set; }

        /// <summary>
        /// 审批人Id
        /// </summary>
        [JsonConverter(typeof(GuidJsonConvert))]
        public Guid? CheckUserId { get; set; }

        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string CheckUserName { get; set; }

        /// <summary>
        /// 审批类型
        /// </summary>
        public ApplyType ApplyType { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public CheckStatus CheckStatus { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string CheckReply { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatDate { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        #endregion

        #region 申请信息
        /// <summary>
        /// 申请人姓名
        /// </summary>       
        public string ApplicantName { get; set; }

        /// <summary>
        /// 申请人电话
        /// </summary>
        public string ApplicantPhone { get; set; }

        /// <summary>
        /// 用车单位ID
        /// </summary>       
        [JsonConverter(typeof(GuidJsonConvert))]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 用车单位名称
        /// </summary>      
        public string DepartmentName { get; set; }


        /// <summary>
        /// 用车人姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用车人电话
        /// </summary>
        public string UserMobile { get; set; }

        /// <summary>
        /// 用车人职称
        /// </summary>
        public string UserTitle { get; set; }

        /// <summary>
        /// 使用人数
        /// </summary>     
        public string UserCount { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>    
        public string CarType { get; set; }

        /// <summary>
        /// 车辆性质
        /// </summary>   
        public CarProperty CarProperty { get; set; }

        /// <summary>
        /// 使用区域
        /// </summary>
        public string UseArea { get; set; }

        /// <summary>
        /// 出发地点
        /// </summary>
        public string StartPoint { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// 申请原因
        /// </summary>  
        public string ApplyReson { get; set; }

        /// <summary>
        /// 预计开始时间
        /// </summary>   
        public DateTime StartPlanTime { get; set; }

        /// <summary>
        /// 预计回队时间
        /// </summary>      
        public DateTime BackPlanTime { get; set; }


        /// <summary>
        /// 附件
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>      
        public ApplyState Status { get; set; }
        #endregion
    }
}
