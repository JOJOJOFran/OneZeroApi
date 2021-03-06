﻿using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Core.Dtos.Permission
{
    public class RoleData : DataDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Remark { get; set; }
    }
}
