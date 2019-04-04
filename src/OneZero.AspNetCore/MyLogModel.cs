using LogDashboard.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.AspNetCore
{
    public class MyLogModel : LogModel
    {
        public string Application { get; set; }
    }
}
