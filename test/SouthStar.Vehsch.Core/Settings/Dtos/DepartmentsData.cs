using Newtonsoft.Json;
using OneZero.Common.Converts;
using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.Setting.Dtos
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
