using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneZero.Application.Models;

namespace SouthStar.VehSch.Api.Areas.Setting.Models
{
    public class Departments : BaseEntity<Guid>
    {

        [Required]
        public Guid ParentDepartmentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; }

        [MaxLength(500)]
        public string Remark { get; set; }


        public int RowNo { get; set; }
    }
}
