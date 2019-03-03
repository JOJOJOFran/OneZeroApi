﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.Setting.Dtos
{
    public class DepartmentData
    {
        public Guid DepartmentId { get; set; }

        public Guid ParentDepartmentId { get; set; }

        public string ParentDepartmentName { get; set; }

        public string DepartmentName { get; set; }

        public string Remark { get; set; }

        public int RowNo { get; set; }
    }
}
