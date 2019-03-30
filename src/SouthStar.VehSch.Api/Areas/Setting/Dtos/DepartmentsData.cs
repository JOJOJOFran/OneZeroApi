using Newtonsoft.Json;
using OneZero.Common.Convert;
using OneZero.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Setting.Dtos
{
    public class DepartmentData:DataDto
    {
        [JsonConverter(typeof(GuidJsonConvert))]
        public Guid Id { get; set; }

        [JsonConverter(typeof(GuidJsonConvert))]
        public Guid? ParentDepartmentId { get; set; }

        public string ParentDepartmentName { get; set; }

        public string DepartmentName { get; set; }

        public string Remark { get; set; }

        public int RowNo { get; set; }
    }
}
